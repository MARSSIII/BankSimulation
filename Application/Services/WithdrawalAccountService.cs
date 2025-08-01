using Abstractions.Repositories;
using Contracts.ResultType;
using Contracts.Service;
using Models.Accounts;

namespace Lab5.Application.Services;

public class WithdrawalAccountService : IWithdrawalsAccountService
{
    private readonly IAccountRepository _repository;

    public WithdrawalAccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public WithdrawResult WithdrawMoney(Account account, int amount)
    {
        if (account is null)
            throw new ArgumentException("Seems, you haven't logged in any account yet");

        if (amount > account.Balance)
            return new WithdrawResult.NotEnoghtMoney();

        account.Balance -= amount;
        _repository.UpdateAmount(account, amount * -1);

        return new WithdrawResult.Success();
    }
}