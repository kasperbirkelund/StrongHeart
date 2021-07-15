using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features
{
    public abstract class CommandFeatureBase<TRequest, TRequestDto> : ICommandFeature<TRequest, TRequestDto>
        where TRequest : IRequest<TRequestDto> 
        where TRequestDto : IRequestDto
    {
        public abstract Task<Result> Execute(TRequest request);
    }
}
