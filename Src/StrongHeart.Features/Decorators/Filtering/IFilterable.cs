using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Filtering;

public interface IFilterable<TResponse> where TResponse : IResponseDto
{
    TResponse GetFilteredItem(IFilterDecisionContext context, TResponse response);
}