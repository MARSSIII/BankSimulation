using Models.Users;

namespace Contracts.Builders;

public interface IUserBuilder
{
    IUserBuilder WithRole(UserRole role);

    public IUserBuilder WithName(string? name);

    public IUserBuilder WithPassword(string? password);

    public IUserBuilder WithId(long id);

    public User Build();
}