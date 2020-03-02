using System.Threading.Tasks;

namespace StrongHeart.Features.Core
{
    public interface IFeature<in TRequest, TResponse> : IFeatureMarker
        where TRequest : IRequest
    {
        Task<TResponse> Execute(TRequest request);
    }
}