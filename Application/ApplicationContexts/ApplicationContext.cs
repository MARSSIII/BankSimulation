using Contracts.ApplicationContexts;
using Models.Users;

namespace Lab5.Application.ApplicationContexts;

public class ApplicationContext : IApplicationContext
{
    public ApplicationContext()
    {
        CurrentMode = UserRole.User;
        CurrentUser = new AtmUser(null, null);
    }

    public UserRole CurrentMode { get; set; }

    public AtmUser CurrentUser { get; set; }
}