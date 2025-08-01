using System.Diagnostics.CodeAnalysis;

namespace Presentation;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}