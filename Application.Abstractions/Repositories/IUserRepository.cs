using Models.Users;

namespace Abstractions.Repositories;

public interface IUserRepository
{
    User? FineUserByUserName(string userName);

    string? FindPasswordByUserName(string userName);

    bool ExistsId(long? id);

    bool ExistsUserName(string? userName);

    void Add(User user);

    void Delete(User user);
}