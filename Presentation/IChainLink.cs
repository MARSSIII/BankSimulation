namespace Presentation;

public interface IChainLink<TRequest, TResult>
{
    TResult Hander(TRequest request);

    IChainLink<TRequest, TResult> AddNext(IChainLink<TRequest, TResult> link);
}