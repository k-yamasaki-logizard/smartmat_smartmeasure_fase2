// ZeroApiOptions.cs
// ZERO API 接続設定

namespace Smart.Api.Options;

/// <summary>
/// ZERO API の接続設定（appsettings の ZERO_API セクション）
/// </summary>
public class ZeroApiOptions
{
    public const string SectionName = "ZERO_API";

    /// <summary>API ベースURL（例: https://api.example/）</summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>APP_KEY</summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>AUTH_KEY</summary>
    public string AuthKey { get; set; } = string.Empty;

    /// <summary>OWNER_ID</summary>
    public string OwnerId { get; set; } = string.Empty;

    /// <summary>AREA_ID</summary>
    public string AreaId { get; set; } = string.Empty;
}
