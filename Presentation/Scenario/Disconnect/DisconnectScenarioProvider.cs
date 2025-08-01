using Contracts.ApplicationContexts;
using Contracts.Service;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.Disconnect;

public class DisconnectScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;

    private readonly IApplicationContext _applicationContext;

    public DisconnectScenarioProvider(IUserService userService, IApplicationContext applicationContext)
    {
        _userService = userService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentUser.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new DisconnectScenario(_userService, _applicationContext);
        return true;
    }
}