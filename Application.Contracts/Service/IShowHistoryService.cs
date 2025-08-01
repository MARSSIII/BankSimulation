using Models.Accounts;

namespace Contracts.Service;

public interface IShowHistoryService
{
    void SeeHistory(Account account);
}