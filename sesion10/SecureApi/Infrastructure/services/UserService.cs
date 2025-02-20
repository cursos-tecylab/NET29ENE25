using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{

    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        return new User
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
    public async Task<bool> ValidateUserCredentials(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        return await _userManager.CheckPasswordAsync(user, password);

    }

    public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user == null) return null;

        return new User
        {
            Id = user.Id,
            Email = user.Email,
            RefreshTokenExpiryDate = user.RefreshTokenExpiryDate
        };
    }

    public async Task UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return;

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryDate = expiryDate;
        await _userManager.UpdateAsync(user);
    }
}