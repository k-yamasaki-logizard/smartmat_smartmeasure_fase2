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
    public async Task<IActionResult> GetSku(
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
    public async Task<IActionResult> UpdateItemPackageWeight(
        [FromBody] ItemPackageWeightRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"ケースバーコード\",\"ケース_重量（SKU単位）\"";
        var importBodyRows = body.Select(b => $"\"{b.ItemId}\",\"{b.CaseBarcode}\",\"{b.CaseWeight}\"");
        var importRequest = new ImportRequest("2115", "0", new string[] { importHeader }.Concat(importBodyRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
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
    public async Task<IActionResult> UpdateItemPackageSize(
        [FromBody] ItemPackageSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"ケースバーコード\",\"ケース_縦（SKU単位）\",\"ケース_横（SKU単位）\",\"ケース_高さ（SKU単位）\"";
        var importBodyRows = body.Select(b => $"\"{b.ItemId}\",\"{b.CaseBarcode}\",\"{b.CaseLength}\",\"{b.CaseWidth}\",\"{b.CaseHeight}\"");
        var importRequest = new ImportRequest("2115", "1", new string[] { importHeader }.Concat(importBodyRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
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
    public async Task<IActionResult> UpdateItemPackageWeightAndSize(
        [FromBody] ItemPackageWeightAndSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"ケースバーコード\",\"ケース_縦（SKU単位）\",\"ケース_横（SKU単位）\",\"ケース_高さ（SKU単位）\",\"ケース_重量（SKU単位）\"";
        var importRows = body.Select(b => $"\"{b.ItemId}\",\"{b.CaseBarcode}\",\"{b.CaseLength}\",\"{b.CaseWidth}\",\"{b.CaseHeight}\",\"{b.CaseWeight}\"");
        var importRequest = new ImportRequest("2115", "2", new string[] { importHeader }.Concat(importRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("item-weight")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateItemWeight(
        [FromBody] ItemWeightRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"重量\"";
        var importBodyRows = body.Select(b => $"\"{b.ItemId}\",\"{b.Weight}\"");
        var importRequest = new ImportRequest("2115", "3", new string[] { importHeader }.Concat(importBodyRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("item-size")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateItemSize(
        [FromBody] ItemSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"縦\",\"横\",\"高さ\"";
        var importBodyRows = body.Select(b => $"\"{b.ItemId}\",\"{b.Length}\",\"{b.Width}\",\"{b.Height}\"");
        var importRequest = new ImportRequest("2115", "4", new string[] { importHeader }.Concat(importBodyRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("item-weight-and-size")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateItemWeightAndSize(
        [FromBody] ItemWeightAndSizeRequest[]? body,
        CancellationToken cancellationToken)
    {
        if (body == null || body.Length == 0)
            return BadRequest(new { error = "At least one item is required." });

        var importHeader = "\"商品ID\",\"重量\",\"縦\",\"横\",\"高さ\"";
        var importBodyRows = body.Select(b => $"\"{b.ItemId}\",\"{b.Weight}\",\"{b.Length}\",\"{b.Width}\",\"{b.Height}\"");
        var importRequest = new ImportRequest("2115", "5", new string[] { importHeader }.Concat(importBodyRows).ToArray());
        var result = await _zeroApiClient.ImportAsync(importRequest, cancellationToken);
        return Ok(result);
    }
}

/// <summary>梱包形態（重量）更新のリクエスト body</summary>
public record ItemPackageWeightRequest(string? ItemId, string? CaseBarcode, string? CaseWeight);

/// <summary>梱包形態（サイズ）更新のリクエスト body</summary>
public record ItemPackageSizeRequest(string? ItemId, string? CaseBarcode, string? CaseLength, string? CaseWidth, string? CaseHeight);

/// <summary>梱包形態（重量とサイズ）更新のリクエスト body</summary>
public record ItemPackageWeightAndSizeRequest(string? ItemId, string? CaseBarcode, string? CaseWeight, string? CaseLength, string? CaseWidth, string? CaseHeight);

/// <summary>商品マスタ（重量）更新のリクエスト body</summary>
public record ItemWeightRequest(string? ItemId, string? Weight);

/// <summary>商品マスタ（サイズ）更新のリクエスト body</summary>
public record ItemSizeRequest(string? ItemId, string? Length, string? Width, string? Height);

/// <summary>商品マスタ（重量とサイズ）更新のリクエスト body</summary>
public record ItemWeightAndSizeRequest(string? ItemId, string? Weight, string? Length, string? Width, string? Height);