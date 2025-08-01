using Abstractions.Repositories;
using Contracts.ApplicationContexts;
using Contracts.ResultType;
using Contracts.Service;
using Lab5.Application.Builders;
using Models.Accounts;
using Models.Transactions;
using Spectre.Console;

namespace Lab5.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountsRepository;

    private readonly IUserRepository _usersRepository;

    private readonly ITransactionsRepository _transactionsRepository;

    public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ITransactionsRepository transactionsRepository)
    {
        _accountsRepository = accountRepository;
        _usersRepository = userRepository;
        _transactionsRepository = transactionsRepository;
    }

    public CreateResult CreateAccount(IApplicationContext context, int pinCode, long userId)
    {
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

    public bool LoginAccount(IApplicationContext context, int pincode)
    {
        if (context.CurrentUser.Account is not null && context.CurrentUser.User is null)
        {
            return false;
        }

        if (context.CurrentUser.User != null)
        {
            Account? account = _accountsRepository.FindAccountByUserId(context.CurrentUser.User.Id, pincode);

            if (account is not null)
            {
                if (account.PinCode == pincode)
                {
                    context.CurrentUser.Account = account;
                    return true;
                }
            }
        }

        return false;
    }

    public void Refill(IApplicationContext context, int amount)
    {
        if (context.CurrentUser.Account is null)
            throw new ArgumentException("Seems, you haven't logged in any account yet");

        context.CurrentUser.Account.Balance += amount;
        _accountsRepository.UpdateAmount(context.CurrentUser.Account, amount);

        _transactionsRepository.Add(context, TransactionType.Refill);
    }

    public WithdrawResult WithdrawMoney(IApplicationContext context, int amount)
    {
        if (context.CurrentUser.Account is null)
            throw new ArgumentException("Seems, you haven't logged in any account yet");

        if (amount > context.CurrentUser.Account.Balance)
            return new WithdrawResult.NotEnoghtMoney();

        context.CurrentUser.Account.Balance -= amount;
        _accountsRepository.UpdateAmount(context.CurrentUser.Account, amount * -1);

        _transactionsRepository.Add(context, TransactionType.Withdraw);

        return new WithdrawResult.Success();
    }

    public void ShowBalance(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        AnsiConsole.WriteLine(account.Balance.ToString());
        AnsiConsole.Ask<string>("OK");
    }

    public void ShowBalance(long accountId)
    {
        Account account = _accountsRepository.FindAccountByAccountId(accountId) ??
                          throw new ArgumentException("Account not found");
        AnsiConsole.WriteLine(account.Balance.ToString());
        AnsiConsole.Ask<string>("OK");
    }

    public void SeeHistory(Account account)
    {
        IList<Transaction>? requestedHistory = _transactionsRepository.GetTransactionsByUserId(account);

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
        AnsiConsole.Ask<string>("OK");
    }
}