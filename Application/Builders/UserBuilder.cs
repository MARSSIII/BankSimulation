using Contracts.Builders;
using Models.Users;

namespace Lab5.Application.Builders;

public class UserBuilder : IUserBuilder
{
    private long _id;

    private string? _name;

    private string? _password;

    private UserRole _role;

    public UserBuilder() { }

    public UserBuilder(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        _id = user.Id;
        _name = user.UserName;
        _role = UserRole.User;
        _password = user.Password;
    }

    public IUserBuilder WithRole(UserRole role)
    {
        _role = role;
        return this;
    }

    public IUserBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public IUserBuilder WithPassword(string? password)
    {
        _password = password;
        return this;
    }

    public IUserBuilder WithId(long id)
    {
        _id = id;
        return this;
    }

    public User Build()
    {
        return new User(_id, _name, _role, _password);
    }
}