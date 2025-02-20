using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/users")]

public class UserController : ControllerBase
{


    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var username = User.Identity.Name;
        return Ok(new { message = $"Bienvenido {username}" });
    }

}