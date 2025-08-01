using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.WithdrawalsAccount;

public class WithdrawalsScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _withdrawalsAccountService;

    private readonly IApplicationContext _applicationContext;

    public WithdrawalsScenarioProvider(
        IAccountService withdrawalsAccountService,
        IApplicationContext applicationContext)
    {
        _withdrawalsAccountService = withdrawalsAccountService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.Account == null)
        {
            scenario = null;
            return false;
        }

        scenario = new WithdrawalsScenario(_withdrawalsAccountService, _applicationContext);
        return true;
    }
}