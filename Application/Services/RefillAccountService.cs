using Abstractions.Repositories;
using Contracts.Service;
using Models.Accounts;

namespace Lab5.Application.Services;

public class RefillAccountService : IRefillAccountService
{
    private readonly IAccountRepository _repository;

    public RefillAccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public void Refill(Account account, int amount)
    {
        if (account is null)
            throw new ArgumentException("Seems, you haven't logged in any account yet");

        account.Balance += amount;
        _repository.UpdateAmount(account, amount);
    }
}