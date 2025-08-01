using Contracts.ApplicationContexts;
using Models.Accounts;
using Models.Transactions;

namespace Abstractions.Repositories;

public interface ITransactionsRepository
{
    void Add(IApplicationContext context, TransactionType transactionType);

    IList<string>? GetInfo(Account account);

    IList<Transaction>? GetTransactionsByUserId(Account account);

    void DeleteByAccountId(long? accountId);

    void DeleteByUserId(long? userId);
}