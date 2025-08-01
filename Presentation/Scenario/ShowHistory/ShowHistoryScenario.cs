using Contracts.ApplicationContexts;
using Contracts.Service;

namespace Presentation.Scenario.ShowHistory;

public class ShowHistoryScenario : IScenario
{
    private readonly IAccountService _showHistoryService;

    private readonly IApplicationContext _applicationContext;

    public ShowHistoryScenario(IAccountService showHistoryService, IApplicationContext applicationContext)
    {
        _showHistoryService = showHistoryService;
        _applicationContext = applicationContext;
    }

    public string Name => "Show History";

    public void Run()
    {
        if (_applicationContext.CurrentUser.Account == null)
            return;

        _showHistoryService.SeeHistory(_applicationContext.CurrentUser.Account);
    }
}