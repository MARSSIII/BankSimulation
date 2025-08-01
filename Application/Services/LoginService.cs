using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Models.Users;

namespace Lab5.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public LoginResult LoginInAdmin(IApplicationContext context, string userName, string? password)
    {
        User? user = _userRepository.FineUserByUserName(userName);

        if (user == null)
            return new LoginResult.UnknownUserName();

        if (userName == "admin")
        {
            if (user.Password != password || password == null)
                throw new ApplicationException("Invalid password for admin.");

            context.CurrentMode = UserRole.Admin;
        }
        else
        {
            context.CurrentMode = UserRole.User;
        }

        context.CurrentUser.User = user;
        return new LoginResult.Success();
    }
}