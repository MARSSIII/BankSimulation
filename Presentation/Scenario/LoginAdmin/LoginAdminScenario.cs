using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.LoginAdmin;

public class LoginAdminScenario : IScenario
{
    private readonly IUserService _loginService;

    private readonly IApplicationContext _applicationContext;

    public LoginAdminScenario(IUserService loginService, IApplicationContext applicationContext)
    {
        _loginService = loginService;
        _applicationContext = applicationContext;
    }

    public string Name => "Login in administrator";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter password: ");

        LoginResult result = _loginService.LoginInAdmin(_applicationContext, password);

        string message = result switch
        {
            LoginResult.Success => "Login successful!",
            LoginResult.UnknownUserName => "Unknown user name",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("OK");
    }
}