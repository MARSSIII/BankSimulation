using Models.Accounts;

namespace Contracts.Builders;

public interface IAccountBuilder
{
    public IAccountBuilder WithAmount(int amount);

    public IAccountBuilder WithPinCode(int pinCode);

    public IAccountBuilder WithId(long id);

    public IAccountBuilder WithUserId(long userId);

    public Account Build();
}