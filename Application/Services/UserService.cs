using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Lab5.Application.Builders;
using Lab5.Application.Configuration.SystemSettings;
using Microsoft.Extensions.Options;
using Models.Users;

namespace Lab5.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _usersRepository;

    private readonly string _passwordAdmin;

    public UserService(IUserRepository userRepository, IOptions<SystemSetting> systemSetting)
    {
        _usersRepository = userRepository;
        _passwordAdmin = systemSetting.Value.SystemPassword;
    }

    public void CreateUser(IApplicationContext context, string? username, string? password)
    {
        User user = new UserBuilder().WithRole(UserRole.User).WithName(username).WithPassword(password).Build();
        _usersRepository.Add(user);

        context.CurrentUser.User = user;
    }

    public LoginResult Login(IApplicationContext context, string userName, string? password)
    {
        User? user = _usersRepository.FineUserByUserName(userName);

        if (user == null)
            return new LoginResult.UnknownUserName();

        context.CurrentMode = UserRole.User;
        context.CurrentUser.User = user;

        return new LoginResult.Success();
    }

    public LoginResult LoginInAdmin(IApplicationContext context, string? password)
    {
        if (_passwordAdmin != password)
            throw new UnauthorizedAccessException();

        context.CurrentMode = UserRole.Admin;
        context.CurrentUser.User = new UserBuilder().WithRole(UserRole.Admin).Build();

        return new LoginResult.Success();
    }

    public void Disconnect(IApplicationContext context)
    {
        context.CurrentUser.User = null;
        context.CurrentUser.Account = null;

        context.CurrentMode = UserRole.User;
    }
}