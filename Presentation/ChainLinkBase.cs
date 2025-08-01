namespace Presentation;

public abstract class ChainLinkBase<TRequest, TResult> : IChainLink<TRequest, TResult>
{
    protected IChainLink<TRequest, TResult>? NextLink { get; private set; }

    public abstract TResult Hander(TRequest request);

    public IChainLink<TRequest, TResult> AddNext(IChainLink<TRequest, TResult> link)
    {
        if (NextLink is not null)
        {
            return NextLink.AddNext(link);
        }

        NextLink = link;
        return link;
    }
}