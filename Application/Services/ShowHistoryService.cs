using Abstractions.Repositories;
using Contracts.Service;
using Models.Accounts;
using Models.Transactions;
using Spectre.Console;

namespace Lab5.Application.Services;

public class ShowHistoryService : IShowHistoryService
{
    private readonly ITransactionsRepository _repository;

    public ShowHistoryService(ITransactionsRepository repository)
    {
        _repository = repository;
    }

    public void SeeHistory(Account account)
    {
        IList<Transaction>? requestedHistory = _repository.GetTransactionsByUserId(account);

        if (requestedHistory is null || requestedHistory.Count == 0)
        {
            AnsiConsole.WriteLine("No operations yet");
        }

        var table = new Table();
        table.AddColumn("Account Id");
        table.AddColumn("Type");
        table.AddColumn("State");

        if (requestedHistory != null)
        {
            foreach (Transaction transaction in requestedHistory)
                table.AddRow(transaction.AccountId.ToString(), transaction.Type.ToString(), transaction.State.ToString());
        }

        AnsiConsole.Write(table);
    }
}