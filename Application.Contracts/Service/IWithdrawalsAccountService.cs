using Contracts.ResultType;
using Models.Accounts;

namespace Contracts.Service;

public interface IWithdrawalsAccountService
{
    WithdrawResult WithdrawMoney(Account account, int amount);
}