using Contracts.ApplicationContexts;
using Contracts.ResultType;

namespace Contracts.Service;

public interface IUserService
{
    void CreateUser(IApplicationContext context, string? username, string? password);

    LoginResult Login(IApplicationContext context, string userName, string? password);

    LoginResult LoginInAdmin(IApplicationContext context, string? password);

    void Disconnect(IApplicationContext context);
}