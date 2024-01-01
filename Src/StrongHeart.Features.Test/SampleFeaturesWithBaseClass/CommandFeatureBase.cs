using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass;

public abstract class CommandFeatureBase<TRequest, TRequestDto> : ICommandFeature<TRequest, TRequestDto>
    where TRequest : IRequest<TRequestDto>
    where TRequestDto : IRequestDto
{
    public abstract Task<Result> Execute(TRequest request);
}