// ZeroApiController.cs
// ZERO API のプロキシエンドポイント。ルール: /zero-api/[path]。server.js の itemPackageWeight / itemPackageSize 相当。

using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Smart.Api.Clients;

namespace Smart.Api.Controllers;

/// <summary>
/// ZERO API を中継するコントローラー。ベースルートは /zero-api/[path]。
/// </summary>
[ApiController]
[Route("zero-api")]
public class ZeroApiController : ControllerBase
{
    private readonly IZeroApiClient _zeroApiClient;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="zeroApiClient">ZERO API クライアント</param>
    public ZeroApiController(IZeroApiClient zeroApiClient)
    {
        _zeroApiClient = zeroApiClient;
    }

    /// <summary>
    /// GET /zero-api/sku（pack_id, barcode クエリ） でバーコードからSKU情報を取得する。
    /// </summary>
    /// <param name="pack_id">梱包形態ID</param>
    /// <param name="barcode">バーコード</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>SKU情報、または 400/404</returns>
    [HttpGet]
    [Route("sku")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromQuery] string pack_id,
        [FromQuery] string barcode,
        CancellationToken cancellationToken)
    {
        var result = await _zeroApiClient.GetSkuAsync(pack_id, barcode.Trim(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// POST /zero-api/item-package-weight で梱包形態（重量）を更新する。複数件を配列で送信可能。
    /// </summary>
    /// <param name="body">itemId, caseBarcode, caseWeight の配列（空の場合は 400）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>更新結果、ERROR_CODE が "0" でない場合は 400</returns>
    [HttpPost]
    [Route("item-package-weight")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostItemPackageWeight(
        [FromBody] ItemPackageWeightRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var items = body.Select(b => new PackageWeightItem(
            b.ItemId ?? "",
            b.CaseBarcode ?? "",
            b.CaseWeight ?? "")).ToList();

        var result = await _zeroApiClient.UpdatePackageWeightAsync(items, cancellationToken);
        return CheckErrorAndReturn(result);
    }

    /// <summary>
    /// POST /zero-api/item-package-size で梱包形態（サイズ）を更新する。複数件を配列で送信可能。
    /// </summary>
    /// <param name="body">itemId, caseBarcode, caseLength, caseWidth, caseHeight の配列（空の場合は 400）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>更新結果、ERROR_CODE が "0" でない場合は 400</returns>
    [HttpPost]
    [Route("item-package-size")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostItemPackageSize(
        [FromBody] ItemPackageSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var items = body.Select(b => new PackageSizeItem(
            b.ItemId ?? "",
            b.CaseBarcode ?? "",
            b.CaseLength ?? "",
            b.CaseWidth ?? "",
            b.CaseHeight ?? "")).ToList();

        var result = await _zeroApiClient.UpdatePackageSizeAsync(items, cancellationToken);
        return CheckErrorAndReturn(result);
    }

    /// <summary>
    /// POST /zero-api/item-package-weight-and-size で梱包形態（重量とサイズ）を更新する。複数件を配列で送信可能。
    /// </summary>
    /// <param name="body">itemId, caseBarcode, caseWeight, caseLength, caseWidth, caseHeight の配列（空の場合は 400）</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>更新結果、ERROR_CODE が "0" でない場合は 400</returns>
    [HttpPost]
    [Route("item-package-weight-and-size")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostItemPackageWeightAndSize(
        [FromBody] ItemPackageWeightAndSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var items = body.Select(b => new PackageWeightAndSizeItem(
            b.ItemId ?? "",
            b.CaseBarcode ?? "",
            b.CaseWeight ?? "",
            b.CaseLength ?? "",
            b.CaseWidth ?? "",
            b.CaseHeight ?? "")).ToList();

        var result = await _zeroApiClient.UpdatePackageWeightAndSizeAsync(items, cancellationToken);
        return CheckErrorAndReturn(result);
    }

    private static IActionResult CheckErrorAndReturn(object? result)
    {
        if (result is JsonElement elem && elem.TryGetProperty("ERROR_CODE", out var code) && code.GetString() != "0")
            return new BadRequestObjectResult(result);
        return new OkObjectResult(result);
    }
}

/// <summary>梱包形態（重量）更新のリクエスト body</summary>
public record ItemPackageWeightRequest(string? ItemId, string? CaseBarcode, string? CaseWeight);

/// <summary>梱包形態（サイズ）更新のリクエスト body</summary>
public record ItemPackageSizeRequest(string? ItemId, string? CaseBarcode, string? CaseLength, string? CaseWidth, string? CaseHeight);

/// <summary>梱包形態（重量とサイズ）更新のリクエスト body</summary>
public record ItemPackageWeightAndSizeRequest(string? ItemId, string? CaseBarcode, string? CaseWeight, string? CaseLength, string? CaseWidth, string? CaseHeight);