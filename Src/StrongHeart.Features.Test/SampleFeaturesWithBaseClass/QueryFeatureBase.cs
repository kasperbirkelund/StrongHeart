using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass;

public abstract class QueryFeatureBase<TRequest, TResponse> : IQueryFeature<TRequest, TResponse>
    where TResponse : class, IResponseDto
    where TRequest : IRequest
{
    public abstract Task<Result<TResponse>> Execute(TRequest request);
}