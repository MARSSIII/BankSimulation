using Models.Accounts;

namespace Contracts.Service;

public interface IRefillAccountService
{
    void Refill(Account account, int amount);
}