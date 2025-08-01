using Models.Accounts;
using Models.Users;

namespace Abstractions.Repositories;

public interface IAccountRepository
{
    Account? FindAccountByAccountId(long accountId);

    User? FindUserByAccountId(long? accountId);

    Account? FindAccountByUserId(long userId, int pincode);

    void UpdateAmount(Account account, int amount);

    void Add(Account account);

    void Delete(Account account);
}