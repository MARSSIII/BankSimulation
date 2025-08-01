namespace Presentation;

public interface IScenario
{
    string Name { get; }

    void Run();
}