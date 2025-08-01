using Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Models.Accounts;
using Models.Users;
using Npgsql;

namespace DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Account? FindAccountByAccountId(long accountId)
    {
        const string sql = $"""
                            select account_id, account_pin, account_amount, user_id
                            from accounts_data
                            where account_id = :accountId;
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

        if (reader.Read() is false)
            return null;

        return new Account(
            reader.GetInt64(0),
            reader.GetInt32(1),
            reader.GetInt32(2),
            reader.GetInt64(3));
    }

    public User? FindUserByAccountId(long? accountId)
    {
        const string sql = """
                           WITH tmp(userId)
                           AS (select user_id FROM accounts_data
                           WHERE account_id = :accountId)

                           SELECT user_id, user_role, user_name, user_password
                           FROM users
                           INNER JOIN tmp AS tmp
                           ON tmp.userId = users.user_id
                           WHERE user_id = userId;
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

        if (reader.Read() is false)
            return null;

        return new User(
            reader.GetInt64(0),
            reader.GetValue(2).ToString(),
            reader.GetFieldValue<UserRole>(1),
            reader.GetValue(3).ToString());
    }

    public Account? FindAccountByUserId(long userId, int pincode)
    {
        const string sql = $"""
                            select account_id, account_pin, account_amount, user_id
                            from accounts_data
                            where user_id = :userId;
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

        if (reader.Read() is false)
            return null;

        return new Account(
            reader.GetInt64(0),
            reader.GetInt32(1),
            reader.GetInt32(2),
            reader.GetInt64(3));
    }

    public void UpdateAmount(Account account, int amount)
    {
        if (account is null) throw new ArgumentException("Operation cannot be done");

        const string sql = $"""
                            update accounts_data
                            set account_amount = account_amount + :amount
                            where account_id = :accountId;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", account.Id)
            .AddParameter("amount", amount);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();
    }

    public void Add(Account account)
    {
        if (account is null)
            throw new ArgumentException("Operation cannot be done");

        const string sql = $"""
                            INSERT INTO accounts_data(account_pin, account_amount, user_id) VALUES(@pinCode, @balance, @user_id)
                            RETURNING account_id;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("@pinCode", account.PinCode)
            .AddParameter("@balance", account.Balance)
            .AddParameter("@user_id", account.UserId);

        using NpgsqlDataReader reader = command.ExecuteReader();
        command.Dispose();

        if (reader.Read() is false)
            throw new ArgumentException("Operation cannot be done");

        account.Id = reader.GetInt64(0);
    }

    public void Delete(Account account)
    {
        if (account is null || account.Id == 0) throw new ArgumentException("Operation cannot be done");
        const string sql = $"""
                            DELETE FROM accounts_data
                            WHERE account_id = :accountId
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountId", account.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();
        command.Dispose();
    }
}