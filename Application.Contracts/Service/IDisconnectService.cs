using Contracts.ApplicationContexts;

namespace Contracts.Service;

public interface IDisconnectService
{
    void Disconnect(IApplicationContext context);
}