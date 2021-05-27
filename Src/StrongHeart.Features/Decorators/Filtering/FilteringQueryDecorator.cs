using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Filtering
{
    //[DebuggerStepThrough]
    public sealed class FilteringQueryDecorator<TRequest, TResponse> : IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public FilteringQueryDecorator(IQueryFeature<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public async Task<Result<TResponse>> Execute(TRequest request)
        {
            Result<TResponse> unfilteredResponse = await _inner.Execute(request);

            if (unfilteredResponse.Status != ResultType.ExecutedSuccessfully)
            {
                return unfilteredResponse;
            }

            IFilterable<TResponse>? filter = GetFilterable();
            if (filter == null)
            {
                return unfilteredResponse;
            }

            IFilterDecisionContext context = new FilterDecisionContext(request.Caller);

            TResponse filteredResponse = filter.GetFilteredItem(context, unfilteredResponse.Value);
            return Result<TResponse>.Success(filteredResponse);
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }

        private IFilterable<TResponse>? GetFilterable()
        {
            return GetInnerFeature() as IFilterable<TResponse>;
        }
    }
}