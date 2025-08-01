using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Models.Accounts;
using Models.Transactions;
using Npgsql;

namespace DataAccess.Repositories;

public class TransactionRepository : ITransactionsRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void Add(IApplicationContext context, TransactionType transactionType)
    {
        if (context is null) throw new ArgumentException("Operation cannot be done");

        const string sql = """
                           INSERT INTO transactions_info(transaction_account, transaction_type, transaction_state, transaction_userId) VALUES(@accountId, @transactionType, @transactionState, @userId);
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand commandSql = new NpgsqlCommand(sql, connection)
            .AddParameter("@accountId", context.CurrentUser?.Account?.Id)
            .AddParameter("@TransactionType", transactionType)
            .AddParameter("@TransactionState", TransactionState.Commit)
            .AddParameter("@userId", context.CurrentUser?.User?.Id);

        using NpgsqlDataReader reader = commandSql.ExecuteReader();

        commandSql.Dispose();
    }

    public IList<string>? GetInfo(Account account)
    {
        const string sql = $"""
                            select transaction_account, transaction_type, transaction_state
                            from transactions_info
                            where transaction_account = :id;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", account.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
            return null;

        var result = new List<string>();

        while (reader.HasRows)
        {
            result.Add($"Accounts {reader.GetInt64(0)}, type {reader.GetString(1)}, state {reader.GetString(2)}");
            if (reader.Read() is false) break;
        }

        return result;
    }

    public IList<Transaction>? GetTransactionsByUserId(Account account)
    {
        const string sql = $"""
                            select transaction_account, transaction_type, transaction_state
                            from transactions_info
                            where transaction_account = :id;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", account.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
            return null;

        var result = new List<Transaction>();
        while (reader.HasRows)
        {
            result.Add(new Transaction(reader.GetInt64(0), reader.GetFieldValue<TransactionType>(1), reader.GetFieldValue<TransactionState>(2)));
            if (reader.Read() is false) break;
        }

        return result;
    }

    public void DeleteByAccountId(long? accountId)
    {
        if (accountId is null) throw new ArgumentException("Operation cannot be done");
        const string sql = $"""
                            DELETE FROM transactions_info
                            WHERE transaction_account = :accountId
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();
    }

    public void DeleteByUserId(long? userId)
    {
        if (userId is null)
            throw new ArgumentException("Operation cannot be done");

        const string sql = $"""
                            DELETE FROM transactions_info
                            WHERE transaction_userid = :userId
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();
    }
}