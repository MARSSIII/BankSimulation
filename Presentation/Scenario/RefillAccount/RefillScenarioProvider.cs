using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.RefillAccount;

public class RefillScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _refillAccountService;

    private readonly IApplicationContext _applicationContext;

    public RefillScenarioProvider(IAccountService refillAccountService, IApplicationContext applicationContext)
    {
        _refillAccountService = refillAccountService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.User == null || _applicationContext.CurrentUser.Account == null)
        {
            scenario = null;
            return false;
        }

        scenario = new RefillAccountScenario(_refillAccountService, _applicationContext);
        return true;
    }
}