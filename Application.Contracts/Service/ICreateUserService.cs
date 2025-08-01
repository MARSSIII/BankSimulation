using Contracts.ApplicationContexts;

namespace Contracts.Service;

public interface ICreateUserService
{
    void CreateUser(IApplicationContext context, string? username);
}