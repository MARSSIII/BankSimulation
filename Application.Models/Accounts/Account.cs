namespace Models.Accounts;

public class Account
{
    public Account(long? id, int pinCode, int balance, long? userId)
    {
        Id = id;
        PinCode = pinCode;
        Balance = balance;
        UserId = userId;
    }

    public long? Id { get; set; }

    public int PinCode { get; set; }

    public int Balance { get; set; }

    public long? UserId { get; set; }
}
