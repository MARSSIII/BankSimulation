using Models.Users;

namespace Contracts.ApplicationContexts;

public interface IApplicationContext
{
    public UserRole CurrentMode { get; set; }

    public AtmUser CurrentUser { get; set; }
}