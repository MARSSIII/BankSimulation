using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.ShowBalance;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _showBalanceService;

    private readonly IApplicationContext _applicationContext;

    public ShowBalanceScenarioProvider(IAccountService showBalanceService, IApplicationContext applicationContext)
    {
        _showBalanceService = showBalanceService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.Account == null)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowBalanceScenario(_showBalanceService, _applicationContext);
        return true;
    }
}