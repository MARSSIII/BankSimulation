using Models.Accounts;

namespace Contracts.Service;

public interface IShowBalanceService
{
    void ShowBalance(Account account);

    void ShowBalance(long accountId);
}