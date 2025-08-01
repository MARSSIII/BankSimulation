using Contracts.ApplicationContexts;
using Contracts.Service;
using Models.Users;

namespace Lab5.Application.Services;

public class DisconnectService : IDisconnectService
{
    public void Disconnect(IApplicationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        context.CurrentUser.User = null;
        context.CurrentUser.Account = null;

        context.CurrentMode = UserRole.User;
    }
}