using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.LoginUser;

public class LoginUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _loginService;

    private readonly IApplicationContext _applicationContext;

    public LoginUserScenarioProvider(IUserService loginService, IApplicationContext applicationContext)
    {
        _loginService = loginService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new LoginUserScenario(_loginService, _applicationContext);
        return true;
    }
}