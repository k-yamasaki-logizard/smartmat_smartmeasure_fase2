// ZeroApiController.cs
// ZERO API のプロキシエンドポイント。ルール: /zero-api/[path]

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
    /// GET /zero-api/sku?barcode= でバーコードからSKU情報を取得する。
    /// </summary>
    /// <param name="path">ルートの path（例: sku）</param>
    /// <param name="barcode">バーコード（path が sku のとき必須）</param>
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
}
