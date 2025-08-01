using Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Models.Users;
using Npgsql;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FineUserByUserName(string userName)
    {
        const string sql = $"""
                            SELECT user_id, user_name, user_role, user_password
                            FROM users
                            WHERE user_name = :userName;
                            """;

        NpgsqlConnection conntection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, conntection)
            .AddParameter("userName", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
        {
            return null;
        }

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetFieldValue<UserRole>(2),
            reader.GetString(3));
    }

    public string? FindPasswordByUserName(string userName)
    {
        const string sql = $"""
                            SELECT user_id, user_name, user_password
                            FROM users
                            WHERE user_name = :username;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("username", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
        {
            return null;
        }

        return reader.GetString(3);
    }

    public bool ExistsId(long? id)
    {
        if (id is null)
        {
            return false;
        }

        const string sql = $"""
                            SELECT user_id
                            from users
                            where user_id = :id;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();
        command.Dispose();

        if (reader.Read() is false)
        {
            return false;
        }

        return true;
    }

    public bool ExistsUserName(string? userName)
    {
        if (userName is null)
        {
            return false;
        }

        const string sql = $"""
                            select user_id
                            from users
                            where user_name = :username;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("username", userName);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
        {
            return false;
        }

        return true;
    }

    public void Add(User user)
    {
        if (((user.UserName is null) && ExistsUserName(user.UserName)) || (user.Id != 0 && ExistsId(user.Id)))
            return;

        const string sql = $"""
                            INSERT INTO users(user_name, user_role, user_password) VALUES(@username, @userRole, @userPassword)
                            RETURNING user_id;
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("@username", user.UserName)
            .AddParameter("@userRole", user.Role)
            .AddParameter("@userPassword", user.Password);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();

        if (reader.Read() is false)
            return;

        user.Id = reader.GetInt64(0);
    }

    public void Delete(User user)
    {
        if (user.Id == 0)
        {
            throw new ArgumentException("Operation cannot be done");
        }

        const string sql = $"""
                            DELETE FROM users
                            WHERE user_id = :userId
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_id", user.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        command.Dispose();
    }
}