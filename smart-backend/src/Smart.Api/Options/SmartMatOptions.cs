// SmartMatOptions.cs
// Smart Mat API 接続設定

namespace Smart.Api.Options;

/// <summary>
/// Smart Mat API の接続設定（appsettings の SMART_MAT_API セクション）
/// </summary>
public class SmartMatOptions
{
    public const string SectionName = "SMART_MAT_API";

    /// <summary>API ベースURL（例: https://api.smartmat.io）</summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>API キー（X-SmartMat-Key ヘッダーに付与）</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>デバイスID（serialId、計測履歴取得の id パラメータ）</summary>
    public string DeviceId { get; set; } = string.Empty;
}
