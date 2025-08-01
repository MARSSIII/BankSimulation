using Contracts.ApplicationContexts;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.RefillAccount;

public class RefillAccountScenario : IScenario
{
    private readonly IAccountService _refillAccountService;

    private readonly IApplicationContext _applicationContext;

    public RefillAccountScenario(IAccountService refillAccountService, IApplicationContext applicationContext)
    {
        _refillAccountService = refillAccountService;
        _applicationContext = applicationContext;
    }

    public string Name => "Refill Account";

    public void Run()
    {
        if (_applicationContext.CurrentUser.Account == null)
            return;

        int amount = AnsiConsole.Ask<int>("How many would you like to refill? Enter: ");

        _refillAccountService.Refill(_applicationContext, amount);
    }
}