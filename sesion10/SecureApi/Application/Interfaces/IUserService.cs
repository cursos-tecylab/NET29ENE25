public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> ValidateUserCredentials(string email, string password);
    Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    Task UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate);
}