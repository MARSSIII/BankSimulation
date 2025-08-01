namespace Contracts.ResultType;

public record CreateResult
{
    private CreateResult() { }

    public sealed record Success : CreateResult;

    public sealed record UnknownUser : CreateResult;
}