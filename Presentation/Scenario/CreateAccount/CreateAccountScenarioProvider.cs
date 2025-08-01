using Contracts.ApplicationContexts;
using Contracts.Service;
using Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.CreateAccount;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _createAccountService;

    private readonly IApplicationContext _applicationContext;

    public CreateAccountScenarioProvider(
        IAccountService createAccountService,
        IApplicationContext applicationContext)
    {
        _createAccountService = createAccountService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentMode is UserRole.Admin || _applicationContext.CurrentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateAccountScenario(_createAccountService,  _applicationContext);
        return true;
    }
}