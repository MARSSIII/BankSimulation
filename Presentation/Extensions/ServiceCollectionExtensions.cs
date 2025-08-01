using Microsoft.Extensions.DependencyInjection;
using Presentation.Scenario.CreateAccount;
using Presentation.Scenario.CreateUser;
using Presentation.Scenario.Disconnect;
using Presentation.Scenario.LoginAccount;
using Presentation.Scenario.LoginAdmin;
using Presentation.Scenario.LoginUser;
using Presentation.Scenario.RefillAccount;
using Presentation.Scenario.ShowBalance;
using Presentation.Scenario.ShowHistory;
using Presentation.Scenario.WithdrawalsAccount;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginAccountScenarioProvider>();

        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateUserScenarioProvider>();

        collection.AddScoped<IScenarioProvider, RefillScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawalsScenarioProvider>();

        collection.AddScoped<IScenarioProvider, DisconnectScenarioProvider>();

        return collection;
    }
}