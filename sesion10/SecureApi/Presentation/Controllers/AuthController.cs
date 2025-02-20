using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenSevice _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(IUserService userService, ITokenSevice tokenSevice, UserManager<ApplicationUser> userManager)
    {
        _userService = userService;
        _tokenService = tokenSevice;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var newUser = new ApplicationUser { Email = model.Email, UserName = model.Email };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { newUser });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userService.GetUserByEmailAsync(model.Email);

        if (user == null || !await _userService.ValidateUserCredentials(model.Email, model.Password))
        {
            return Unauthorized("Invalid Credentials");
        }

        var token = _tokenService.GenerateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _userService.UpdateRefreshTokenAsync(user.Id, refreshToken.Token, refreshToken.ExpiryDate);

        return Ok(new { token, refreshToken = refreshToken.Token });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var user = await _userService.GetUserByRefreshTokenAsync(refreshToken);
        if (user == null || user.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            return Unauthorized("Invalid or expired refresh token");
        }

        var newAccessToken = _tokenService.GenerateJwtToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        await _userService.UpdateRefreshTokenAsync(user.Id, newRefreshToken.Token, newRefreshToken.ExpiryDate);

        return Ok(new { token = newAccessToken, refreshToken = newRefreshToken.Token });
    }

}