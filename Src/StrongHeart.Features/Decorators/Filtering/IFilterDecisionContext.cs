using StrongHeart.Core.Security;

namespace StrongHeart.Features.Decorators.Filtering;

public interface IFilterDecisionContext
{
    ICaller Caller { get; }
}