using Microsoft.AspNetCore.Mvc;

namespace Smart.Api.Controllers;

/// <summary>
/// Hello World エンドポイントを提供するコントローラー
/// </summary>
[ApiController]
[Route("")]
public class HelloController : ControllerBase
{
    /// <summary>
    /// ルートパスでHello Worldを返す
    /// </summary>
    /// <returns>Hello Worldメッセージ</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World! This is Smart API.");
    }
}
