// SmartMatApiClient.cs
// Smart Mat API 呼び出しの実装（smart-mat-api-client.js 準拠: fetchLatestMeasureHistory）

using System.Text.Json;
using Microsoft.Extensions.Options;
using Smart.Api.Options;

namespace Smart.Api.Clients;

/// <summary>
/// Smart Mat API を呼び出す HTTP クライアントの実装。
/// 最新計測履歴の取得（v1/device/history）を行う。
/// </summary>
public class SmartMatApiClient : ISmartMatApiClient
{
    private readonly HttpClient _httpClient;
    private readonly SmartMatOptions _options;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="httpClient">HttpClient</param>
    /// <param name="options">Smart Mat API 設定</param>
    public SmartMatApiClient(HttpClient httpClient, IOptions<SmartMatOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    private string BaseUrl => _options.BaseUrl?.TrimEnd('/') ?? "";

    /// <inheritdoc />
    public async Task<object?> FetchLatestMeasureHistoryAsync(CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/v1/device/history?id={Uri.EscapeDataString(_options.DeviceId ?? "")}&limit=1";
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-SmartMat-Key", _options.ApiKey ?? "");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Smart Mat API error: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(json))
            return null;

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        if (!root.TryGetProperty("measureHistories", out var histories) || histories.ValueKind != JsonValueKind.Array)
            return null;
        if (histories.GetArrayLength() == 0)
            return null;

        return JsonSerializer.Deserialize<JsonElement>(histories[0].GetRawText());
    }

    public async Task<object?> FetchStockInfoAsync(CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/v1/device/info?id={Uri.EscapeDataString(_options.DeviceId ?? "")}";
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-SmartMat-Key", _options.ApiKey ?? "");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Smart Mat API error: {response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(json))
            return null;

        return JsonSerializer.Deserialize<JsonElement>(json);
    }
}
