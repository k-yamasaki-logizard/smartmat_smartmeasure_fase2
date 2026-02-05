// IZeroApiClient.cs
// ZERO API 呼び出しのインターフェース（zero-api-client.js 準拠）

using System.Collections.Generic;

namespace Smart.Api.Clients;

/// <summary>
/// インポート時のリクエスト用
/// ImportCsvRowsにはヘッダを含める
/// </summary>
public class ImportRequest
{
    public string FileId { get; }
    public string PtrnId { get; }
    public IReadOnlyList<string> ImportCsvRows { get; }

    public ImportRequest(string fileId, string ptrnId, IReadOnlyList<string> csvRows)
    {
        FileId = fileId;
        PtrnId = ptrnId;
        ImportCsvRows = csvRows;
    }
}

/// <summary>
/// ZERO API を呼び出すクライアントのインターフェース
/// </summary>
public interface IZeroApiClient
{
    /// <summary>
    /// 認証して fuelfid を取得する（login/login/keylogin）
    /// </summary>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>ログイン応答（ERROR_CODE, DATA.LOGIN_INFO 等）</returns>
    Task<object?> AuthAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 梱包形態(重量)を更新する（common/import/import, FILE_ID 2115, PTRN_ID 0）。複数件を 1 リクエストで送信可能。
    /// </summary>
    /// <param name="items">更新する件の一覧（空の場合は ArgumentException）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task<object?> ImportAsync(ImportRequest importRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// バーコードからSKU情報を取得する（zero-api-client.js には未実装、コントローラー用）
    /// </summary>
    /// <param name="pack_id">梱包形態ID</param>
    /// <param name="barcode">バーコード</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>SKU情報（JSON相当のオブジェクト）、未取得時は null</returns>
    Task<object?> GetSkuAsync(string pack_id, string barcode, CancellationToken cancellationToken = default);

}
