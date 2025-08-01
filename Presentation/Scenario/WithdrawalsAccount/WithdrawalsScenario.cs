using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Spectre.Console;

namespace Presentation.Scenario.WithdrawalsAccount;

public class WithdrawalsScenario : IScenario
{
    private readonly IAccountService _accountService;

    private readonly IApplicationContext _applicationContext;

    public WithdrawalsScenario(IAccountService accountService, IApplicationContext applicationContext)
    {
        _accountService = accountService;
        _applicationContext = applicationContext;
    }

    public string Name => "Withdrawals account";

    public void Run()
    {
        if (_applicationContext.CurrentUser.Account == null)
            return;

        int amount = AnsiConsole.Ask<int>("Enter the amount : ");

        WithdrawResult result = _accountService.WithdrawMoney(_applicationContext, amount);

        string message = result switch
        {
            WithdrawResult.Success => "Withdrawal successful!",
            WithdrawResult.NotEnoghtMoney => "Withdrawal failed, not enough money!",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("OK");
    }
}