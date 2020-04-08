using StrongHeart.Core.Security;

namespace StrongHeart.Features.Decorators.Filtering
{
    internal class FilterDecisionContext : IFilterDecisionContext
    {
        public FilterDecisionContext(ICaller caller)
        {
            Caller = caller;
        }

        public ICaller Caller { get; }
    }
}