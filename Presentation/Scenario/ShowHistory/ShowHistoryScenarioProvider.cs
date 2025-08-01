using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.ShowHistory;

public class ShowHistoryScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _showHistoryService;

    private readonly IApplicationContext _applicationContext;

    public ShowHistoryScenarioProvider(IAccountService showHistoryService, IApplicationContext applicationContext)
    {
        _showHistoryService = showHistoryService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.Account == null)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowHistoryScenario(_showHistoryService, _applicationContext);
        return true;
    }
}