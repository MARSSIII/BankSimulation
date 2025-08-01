using Contracts.ApplicationContexts;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.CreateUser;

public class CreateUserScenario : IScenario
{
    private readonly IUserService _createUserService;

    private readonly IApplicationContext _applicationContext;

    public string Name => "Create User";

    public CreateUserScenario(IUserService createUserService, IApplicationContext applicationContext)
    {
        _createUserService = createUserService;
        _applicationContext = applicationContext;
    }

    public void Run()
    {
        string userName = AnsiConsole.Ask<string>("Enter user name : ");
        string password = AnsiConsole.Ask<string>("Enter password : ");

        _createUserService.CreateUser(_applicationContext, userName, password);

        AnsiConsole.WriteLine("Success create user");
        AnsiConsole.Ask<string>("OK");
    }
}