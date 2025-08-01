using Contracts.ApplicationContexts;
using Contracts.Service;
using Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.LoginAdmin;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private readonly IUserService _loginService;

    private readonly IApplicationContext _applicationContext;

    public LoginAdminScenarioProvider(IUserService loginService, IApplicationContext applicationContext)
    {
        _loginService = loginService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentMode is UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAdminScenario(_loginService, _applicationContext);
        return true;
    }
}