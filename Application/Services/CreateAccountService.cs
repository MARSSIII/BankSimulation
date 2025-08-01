using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Lab5.Application.Builders;
using Models.Accounts;

namespace Lab5.Application.Services;

public class CreateAccountService : ICreateAccountService
{
    private readonly IAccountRepository _accountsRepository;
    private readonly IUserRepository _usersRepository;

    public CreateAccountService(IAccountRepository accountsRepository, IUserRepository usersRepository)
    {
        _accountsRepository = accountsRepository;
        _usersRepository = usersRepository;
    }

    public CreateResult CreateAccount(IApplicationContext context, int pinCode, long userId)
    {
        ArgumentNullException.ThrowIfNull(context);

        bool existsUser = _usersRepository.ExistsId(userId);

        if (!existsUser)
        {
            return new CreateResult.UnknownUser();
        }

        Account account = new AccountBuilder().WithPinCode(pinCode).WithAmount(0).WithUserId(userId).Build();

        _accountsRepository.Add(account);

        if (context.CurrentUser is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.CurrentUser.Account = account;
        return new CreateResult.Success();
    }
}