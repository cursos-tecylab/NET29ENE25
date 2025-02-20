public interface ITokenSevice {
    string GenerateJwtToken(User user);
    RefreshToken GenerateRefreshToken();
}