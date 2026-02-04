// SmartMatController.cs
// Smart Mat API のプロキシエンドポイント。server.js の GET /api/latestMeasureHistory に相当。

using Microsoft.AspNetCore.Mvc;
using Smart.Api.Clients;

namespace Smart.Api.Controllers;

/// <summary>
/// Smart Mat API を中継するコントローラー。ベースルートは /api。
/// </summary>
[ApiController]
[Route("smartmat-api")]
public class SmartMatController : ControllerBase
{
    private readonly ISmartMatApiClient _smartMatApiClient;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="smartMatApiClient">Smart Mat API クライアント</param>
    public SmartMatController(ISmartMatApiClient smartMatApiClient)
    {
        _smartMatApiClient = smartMatApiClient;
    }

    /// <summary>
    /// GET /api/latest-measure-history で最新の計測履歴を1件取得する。
    /// </summary>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>最新計測履歴、存在しない場合は 404</returns>
    [HttpGet]
    [Route("latest-measure-history")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLatestMeasureHistory(CancellationToken cancellationToken)
    {
        var result = await _smartMatApiClient.FetchLatestMeasureHistoryAsync(cancellationToken);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet]
    [Route("stock-info")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStockInfo(CancellationToken cancellationToken)
    {
        var result = await _smartMatApiClient.FetchStockInfoAsync(cancellationToken);
        return Ok(result);
    }
}
