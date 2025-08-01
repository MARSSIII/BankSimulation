using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAccountService _createAccountService;

    private readonly IApplicationContext _applicationContext;

    public CreateAccountScenario(IAccountService createAccountService, IApplicationContext applicationContext)
    {
        _createAccountService = createAccountService;
        _applicationContext = applicationContext;
    }

    public string Name => "Create account";

    public void Run()
    {
        if (_applicationContext.CurrentUser?.User == null)
            return;

        int pincode = AnsiConsole.Ask<int>("Enter pincode: ");

        CreateResult result = _createAccountService.CreateAccount(_applicationContext, pincode, _applicationContext.CurrentUser.User.Id);

        string message = result switch
        {
            CreateResult.Success => "Account created successfully.",
            CreateResult.UnknownUser => "Unknown user",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("OK");
    }
}