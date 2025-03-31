using Microsoft.AspNetCore.Mvc;


/// <summary>
/// Health Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HealthController : Controller
{
  /// <summary>
  ///  Get API health endpoint.
  /// </summary>
  /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
  [HttpGet]
  public async Task<ActionResult> GetHealth()
  {
    return Ok("ok");
  }
}