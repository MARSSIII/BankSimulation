using Contracts.ApplicationContexts;
using Contracts.ResultType;

namespace Contracts.Service;

public interface ICreateAccountService
{
    CreateResult CreateAccount(IApplicationContext context, int pinCode, long userId);
}