using Contracts.ApplicationContexts;
using Contracts.Service;
using Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.LoginAccount;

public class LoginAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;

    private readonly IApplicationContext _applicationContext;

    public LoginAccountScenarioProvider(IAccountService accountService, IApplicationContext applicationContext)
    {
        _accountService = accountService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.User == null || _applicationContext.CurrentUser.Account != null || _applicationContext.CurrentMode is UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAccountScenario(_accountService, _applicationContext);
        return true;
    }
}