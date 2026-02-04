// ISmartMatApiClient.cs
// Smart Mat API 呼び出しのインターフェース（smart-mat-api-client.js 準拠）

namespace Smart.Api.Clients;

/// <summary>
/// Smart Mat API を呼び出すクライアントのインターフェース
/// </summary>
public interface ISmartMatApiClient
{
    /// <summary>
    /// 最新の計測履歴を1件取得する（v1/device/history?id=...&limit=1）
    /// </summary>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>measureHistories[0] に相当するオブジェクト、存在しない場合は null</returns>
    Task<object?> FetchLatestMeasureHistoryAsync(CancellationToken cancellationToken = default);
}
