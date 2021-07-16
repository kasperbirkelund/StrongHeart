using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Filtering
{
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

            IFilterDecisionContext context = new FilterDecisionContext(request.Caller);
            IFilterable<TResponse> filter = GetFilter();
            TResponse filteredResponse = filter.GetFilteredItem(context, unfilteredResponse.Value);
            return Result<TResponse>.Success(filteredResponse);
        }

        private IFilterable<TResponse> GetFilter()
        {
            IFilterable<TResponse>? filter = this.GetInnerMostFeature() as IFilterable<TResponse>;
            if (filter == null)
            {
                throw new ArgumentException("IFilterable<> has not been implemented on the feature");
            }

            return filter;
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }
    }
}