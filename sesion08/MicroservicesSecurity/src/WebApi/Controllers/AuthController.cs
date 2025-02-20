using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var token = _userService.Authenticate(request.Username, request.Password);
        if (token == null)
            return Unauthorized(new { message = "Credenciales invalidad" });

        return Ok(new { token });
    }

}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}