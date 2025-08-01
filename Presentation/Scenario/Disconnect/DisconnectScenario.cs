using Contracts.ApplicationContexts;
using Contracts.Service;

namespace Presentation.Scenario.Disconnect;

public class DisconnectScenario : IScenario
{
    private readonly IUserService _userService;

    private readonly IApplicationContext _applicationContext;

    public DisconnectScenario(IUserService userService, IApplicationContext applicationContext)
    {
        _userService = userService;
        _applicationContext = applicationContext;
    }

    public string Name => "Disconnect";

    public void Run()
    {
        _userService.Disconnect(_applicationContext);
    }
}