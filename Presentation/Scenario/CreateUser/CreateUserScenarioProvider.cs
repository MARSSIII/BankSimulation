using Contracts.ApplicationContexts;
using Contracts.Service;
using Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Scenario.CreateUser;

public class CreateUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _createUserService;

    private readonly IApplicationContext _applicationContext;

    public CreateUserScenarioProvider(IUserService createUserService, IApplicationContext applicationContext)
    {
        _createUserService = createUserService;
        _applicationContext = applicationContext;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_applicationContext.CurrentMode is UserRole.User)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateUserScenario(_createUserService, _applicationContext);
        return true;
    }
}