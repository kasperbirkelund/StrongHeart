using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Filtering
{
    public interface IFilterDecisionContext
    {
        ICaller Caller { get; }
    }
}