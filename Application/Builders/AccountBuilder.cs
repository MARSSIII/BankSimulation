using Contracts.Builders;
using Models.Accounts;

namespace Lab5.Application.Builders;

public class AccountBuilder : IAccountBuilder
{
    private long? _id;

    private int _pinCode;

    private int _amount;

    private long? _userId;

    public AccountBuilder()
    {
    }

    public AccountBuilder(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _id = account.Id;
        _pinCode = account.PinCode;
        _amount = account.Balance;
        _userId = account.UserId;
    }

    public IAccountBuilder WithAmount(int amount)
    {
        _amount = amount;
        return this;
    }

    public IAccountBuilder WithPinCode(int pinCode)
    {
        _pinCode = pinCode;
        return this;
    }

    public IAccountBuilder WithId(long id)
    {
        _id = id;
        return this;
    }

    public IAccountBuilder WithUserId(long userId)
    {
        _userId = userId;
        return this;
    }

    public Account Build()
    {
        return new Account(_id, _pinCode, _amount, _userId);
    }
}