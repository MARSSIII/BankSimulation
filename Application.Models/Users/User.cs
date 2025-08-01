namespace Models.Users;

public class User
{
    public User(long id, string? userName, UserRole role, string? password)
    {
        Id = id;
        UserName = userName;
        Role = role;
        Password = password;
    }

    public long Id { get; set; }

    public string? UserName { get; }

    public UserRole Role { get; }

    public string? Password { get; }
}