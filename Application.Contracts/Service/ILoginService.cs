using Contracts.ApplicationContexts;
using Contracts.ResultType;

namespace Contracts.Service;

public interface ILoginService
{
    LoginResult LoginInAdmin(IApplicationContext context, string userName, string? password);
}