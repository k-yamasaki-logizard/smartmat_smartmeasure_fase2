// ZeroApiClient.cs
// ZERO API 呼び出しの実装（zero-api-client.js 準拠: auth, updatePackageWeight, updatePackageSize, getSku）

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Smart.Api.Options;

namespace Smart.Api.Clients;

/// <summary>
/// ZERO API を呼び出す HTTP クライアントの実装。
/// 認証で fuelfid を取得し、以降の import 呼び出しで Cookie を付与する。
/// </summary>
public class ZeroApiClient : IZeroApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ZeroApiOptions _options;
    private string? _sessionCookie;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="httpClient">HttpClient</param>
    /// <param name="options">ZERO API 設定</param>
    public ZeroApiClient(HttpClient httpClient, IOptions<ZeroApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    private string BaseUrl => _options.BaseUrl?.TrimEnd('/') ?? "";

    /// <inheritdoc />
    public async Task<object?> AuthAsync(CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/login/login/keylogin";
        var body = JsonSerializer.Serialize(new
        {
            APP_KEY = _options.AppKey,
            AUTH_KEY = _options.AuthKey,
            OWNER_ID = _options.OwnerId,
            AREA_ID = _options.AreaId
        });
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _httpClient.PostAsync(url, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Login failed: {response.StatusCode}");

        _sessionCookie = ParseFuelfidFromSetCookie(response);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<JsonElement>(json);
    }

    /// <inheritdoc />
    public async Task<object?> UpdatePackageWeightAsync(string itemId, string caseBarcode, string caseWeight, CancellationToken cancellationToken = default)
    {
        await EnsureAuthenticatedAsync(cancellationToken);

        var url = $"{BaseUrl}/common/import/import";
        var body = JsonSerializer.Serialize(new
        {
            OWNER_ID = _options.OwnerId,
            AREA_ID = _options.AreaId,
            ONLY_AREA_IMPORT_FLG = "1",
            FILE_ID = "2115",
            PTRN_ID = "0",
            ERROR_DETAIL = "1",
            IMPORT_DATA = new[]
            {
                "\"商品ID\",\"ケースバーコード\",\"ケース_重量（SKU単位）\"",
                $"\"{EscapeCsvField(itemId)}\",\"{EscapeCsvField(caseBarcode)}\",\"{EscapeCsvField(caseWeight ?? "")}\""
            }
        });
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Cookie", _sessionCookie);
        request.Content = new StringContent(body, Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Update item package weight failed: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<JsonElement>(json);
    }

    /// <inheritdoc />
    public async Task<object?> UpdatePackageSizeAsync(string itemId, string caseBarcode, string caseLength, string caseWidth, string caseHeight, CancellationToken cancellationToken = default)
    {
        await EnsureAuthenticatedAsync(cancellationToken);

        var url = $"{BaseUrl}/common/import/import";
        var body = JsonSerializer.Serialize(new
        {
            OWNER_ID = _options.OwnerId,
            AREA_ID = _options.AreaId,
            ONLY_AREA_IMPORT_FLG = "1",
            FILE_ID = "2115",
            PTRN_ID = "1",
            ERROR_DETAIL = "1",
            IMPORT_DATA = new[]
            {
                "\"商品ID\",\"ケースバーコード\",\"ケース_縦（SKU単位）\",\"ケース_横（SKU単位）\",\"ケース_高さ（SKU単位）\"",
                $"\"{EscapeCsvField(itemId)}\",\"{EscapeCsvField(caseBarcode)}\",\"{EscapeCsvField(caseLength ?? "")}\",\"{EscapeCsvField(caseWidth ?? "")}\",\"{EscapeCsvField(caseHeight ?? "")}\""
            }
        });
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Cookie", _sessionCookie);
        request.Content = new StringContent(body, Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Update item package size failed: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<JsonElement>(json);
    }

    /// <inheritdoc />
    public async Task<object?> GetSkuAsync(string pack_id, string barcode, CancellationToken cancellationToken = default)
    {
        await EnsureAuthenticatedAsync(cancellationToken);

        var url = $"{BaseUrl}/common/master/getsku";
        var body = JsonSerializer.Serialize(new
        {
            OWNER_ID = _options.OwnerId,
            PACK_ID = pack_id,
            BARCODE = barcode
        });

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(json))
            return null;

        return JsonSerializer.Deserialize<JsonElement>(json);
    }

    private async Task EnsureAuthenticatedAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_sessionCookie))
            await AuthAsync(cancellationToken);
    }

    /// <summary>
    /// Set-Cookie ヘッダから fuelfid を取得（Cookie ヘッダ用に "fuelfid=値" を返す）
    /// </summary>
    private static string? ParseFuelfidFromSetCookie(HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues("Set-Cookie", out var values))
            return null;

        foreach (var header in values)
        {
            var parts = header.Split(';');
            var first = parts[0].Trim();
            if (first.StartsWith("fuelfid=", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(first, "fuelfid=deleted", StringComparison.OrdinalIgnoreCase))
                return first;
        }

        return null;
    }

    private static string EscapeCsvField(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Replace("\"", "\"\"");
    }
}
