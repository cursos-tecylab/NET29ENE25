public class UserRepository : IUserRepository
{
    private readonly List<User> _users;

    public UserRepository()
    {
        _users = new List<User> {
             new User {Id = "1", Username= "admin", PasswordHash = "admin123", Role = "Admin"},
               new User {Id = "2", Username= "user", PasswordHash = "user123", Role = "USer"}
        };
    }


    public User GetUserByUsername(string username)
    {
        return _users.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
}