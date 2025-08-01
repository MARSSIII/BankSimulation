using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.Service;
using Lab5.Application.Builders;
using Models.Users;

namespace Lab5.Application.Services;

public class CreateUserService : ICreateUserService
{
    private readonly IUserRepository _usersRepository;

    public CreateUserService(IUserRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public void CreateUser(IApplicationContext context, string? username)
    {
        ArgumentNullException.ThrowIfNull(context);

        User user = new UserBuilder().WithRole(UserRole.User).WithName(username).Build();
        _usersRepository.Add(user);

        context.CurrentUser.User = user;
    }
}