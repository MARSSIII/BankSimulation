using Contracts.ApplicationContexts;
using Contracts.Service;

namespace Presentation.Scenario.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IAccountService _showBalanceService;

    private readonly IApplicationContext _applicationContext;

    public string Name => "Show balance";

    public ShowBalanceScenario(IAccountService showBalanceService, IApplicationContext applicationContext)
    {
        _showBalanceService = showBalanceService;
        _applicationContext = applicationContext;
    }

    public void Run()
    {
        if (_applicationContext.CurrentUser.Account == null)
            return;

        _showBalanceService.ShowBalance(_applicationContext.CurrentUser.Account);
    }
}