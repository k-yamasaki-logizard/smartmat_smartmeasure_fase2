// IZeroApiClient.cs
// ZERO API 呼び出しのインターフェース（zero-api-client.js 準拠）

namespace Smart.Api.Clients;

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
    /// 梱包形態(重量)を更新する（common/import/import, FILE_ID 2115, PTRN_ID 0）
    /// </summary>
    /// <param name="itemId">商品ID</param>
    /// <param name="caseBarcode">ケースバーコード</param>
    /// <param name="caseWeight">ケース_重量（SKU単位）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task<object?> UpdatePackageWeightAsync(string itemId, string caseBarcode, string caseWeight, CancellationToken cancellationToken = default);

    /// <summary>
    /// 梱包形態(サイズ)を更新する（common/import/import, FILE_ID 2115, PTRN_ID 1）
    /// </summary>
    /// <param name="itemId">商品ID</param>
    /// <param name="caseBarcode">ケースバーコード</param>
    /// <param name="caseLength">ケース_縦（SKU単位）</param>
    /// <param name="caseWidth">ケース_横（SKU単位）</param>
    /// <param name="caseHeight">ケース_高さ（SKU単位）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task<object?> UpdatePackageSizeAsync(string itemId, string caseBarcode, string caseLength, string caseWidth, string caseHeight, CancellationToken cancellationToken = default);

    /// <summary>
    /// バーコードからSKU情報を取得する（zero-api-client.js には未実装、コントローラー用）
    /// </summary>
    /// <param name="pack_id">梱包形態ID</param>
    /// <param name="barcode">バーコード</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>SKU情報（JSON相当のオブジェクト）、未取得時は null</returns>
    Task<object?> GetSkuAsync(string pack_id, string barcode, CancellationToken cancellationToken = default);
}
