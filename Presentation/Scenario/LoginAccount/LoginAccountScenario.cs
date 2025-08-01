using Contracts.ApplicationContexts;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.LoginAccount;

public class LoginAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    private readonly IApplicationContext _applicationContext;

    public LoginAccountScenario(IAccountService accountService, IApplicationContext applicationContext)
    {
        _accountService = accountService;
        _applicationContext = applicationContext;
    }

    public string Name => "Login account";

    public void Run()
    {
        int pinCode = AnsiConsole.Ask<int>("Enter pin code: ");

        bool result = _accountService.LoginAccount(_applicationContext, pinCode);

        string message = result switch
        {
            true => "Successfully logged in",
            false => "Invalid pin code",
        };
    }
}