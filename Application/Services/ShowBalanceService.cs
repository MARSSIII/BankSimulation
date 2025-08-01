using Abstractions.Repositories;
using Contracts.Service;
using Models.Accounts;
using Spectre.Console;

namespace Lab5.Application.Services;

public class ShowBalanceService : IShowBalanceService
{
    private readonly IAccountRepository _repository;

    public ShowBalanceService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public void ShowBalance(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        AnsiConsole.WriteLine(account.Balance.ToString());
    }

    public void ShowBalance(long accountId)
    {
        Account account = _repository.FindAccountByAccountId(accountId) ??
                          throw new ArgumentException("Account not found");
        AnsiConsole.WriteLine(account.Balance.ToString());
    }
}