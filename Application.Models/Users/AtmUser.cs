using Models.Accounts;

namespace Models.Users;

public class AtmUser
{
    public AtmUser(Account? account, User? user)
    {
        Account = account;
        User = user;
    }

    public Account? Account { get; set; }

    public User? User { get; set; }
}