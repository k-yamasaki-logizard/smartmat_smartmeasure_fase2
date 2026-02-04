// IZeroApiClient.cs
// ZERO API 呼び出しのインターフェース（zero-api-client.js 準拠）

using System.Collections.Generic;

namespace Smart.Api.Clients;

/// <summary>梱包形態（重量）更新の 1 件分</summary>
public record PackageWeightItem(string ItemId, string CaseBarcode, string CaseWeight);

/// <summary>梱包形態（サイズ）更新の 1 件分</summary>
public record PackageSizeItem(string ItemId, string CaseBarcode, string CaseLength, string CaseWidth, string CaseHeight);

/// <summary>梱包形態（重量とサイズ）更新の 1 件分</summary>
public record PackageWeightAndSizeItem(string ItemId, string CaseBarcode, string CaseWeight, string CaseLength, string CaseWidth, string CaseHeight);

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
    Task<object?> UpdatePackageWeightAsync(IReadOnlyList<PackageWeightItem> items, CancellationToken cancellationToken = default);

    /// <summary>
    /// 梱包形態(サイズ)を更新する（common/import/import, FILE_ID 2115, PTRN_ID 1）。複数件を 1 リクエストで送信可能。
    /// </summary>
    /// <param name="items">更新する件の一覧（空の場合は ArgumentException）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task<object?> UpdatePackageSizeAsync(IReadOnlyList<PackageSizeItem> items, CancellationToken cancellationToken = default);

    /// <summary>
    /// バーコードからSKU情報を取得する（zero-api-client.js には未実装、コントローラー用）
    /// </summary>
    /// <param name="pack_id">梱包形態ID</param>
    /// <param name="barcode">バーコード</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>SKU情報（JSON相当のオブジェクト）、未取得時は null</returns>
    Task<object?> GetSkuAsync(string pack_id, string barcode, CancellationToken cancellationToken = default);

    /// <summary>
    /// 梱包形態（重量とサイズ）を更新する（common/import/import, FILE_ID 2115, PTRN_ID 2）。複数件を 1 リクエストで送信可能。
    /// </summary>
    /// <param name="items">更新する件の一覧（空の場合は ArgumentException）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task<object?> UpdatePackageWeightAndSizeAsync(IReadOnlyList<PackageWeightAndSizeItem> items, CancellationToken cancellationToken = default);
}
