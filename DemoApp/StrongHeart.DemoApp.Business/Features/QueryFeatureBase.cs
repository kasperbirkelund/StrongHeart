using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features
{
    /// <summary>
    /// This class is a project specific class where you can apply your project specific decorators.
    /// Make sure that all your features inherent this base class to be able to make streamlined behaviour
    /// </summary>
    public abstract class QueryFeatureBase<TRequest, TResponse> : FeatureBase, IQueryFeature<TRequest, TResponse>
        where TResponse : class, IResponseDto
        where TRequest : IRequest
    {
        public abstract Task<Result<TResponse>> Execute(TRequest request);
    }
}