using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Models.Accounts;

namespace Contracts.Service;

public interface IAccountService
{
    CreateResult CreateAccount(IApplicationContext context, int pinCode, long userId);

    bool LoginAccount(IApplicationContext context, int pincode);

    void Refill(IApplicationContext context, int amount);

    WithdrawResult WithdrawMoney(IApplicationContext context, int amount);

    void ShowBalance(Account account);

    void ShowBalance(long accountId);

    public void SeeHistory(Account account);
}