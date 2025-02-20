using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
    }

    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryDate { get; set; }
}