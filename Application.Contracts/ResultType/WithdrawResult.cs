namespace Contracts.ResultType;

public record WithdrawResult
{
    private WithdrawResult() { }

    public sealed record Success : WithdrawResult;

    public sealed record NotEnoghtMoney : WithdrawResult;
}