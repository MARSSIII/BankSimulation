using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.LoginUser;

public class LoginUserScenario : IScenario
{
    private readonly IUserService _loginService;

    private readonly IApplicationContext _applicationContext;

    public LoginUserScenario(IUserService loginService, IApplicationContext applicationContext)
    {
        _loginService = loginService;
        _applicationContext = applicationContext;
    }

    public string Name => "Login in User";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username: ");
        string password = AnsiConsole.Ask<string>("Enter your password: ");

        LoginResult result = _loginService.Login(_applicationContext, username, password);

        string message = result switch
        {
            LoginResult.Success => "Login successful!",
            LoginResult.UnknownUserName => "Unknown username",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("OK");
    }
}