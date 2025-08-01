namespace Contracts.ResultType;

public record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record UnknownUserName : LoginResult;
}