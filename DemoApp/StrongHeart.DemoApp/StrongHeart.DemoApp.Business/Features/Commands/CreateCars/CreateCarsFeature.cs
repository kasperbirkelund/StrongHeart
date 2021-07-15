using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Commands.CreateCars
{
    public class CreateCarsFeature : CommandFeatureBase<CreateCarsRequest, CreateCarsRequestDto>
    {
        public override Task<Result> Execute(CreateCarsRequest request)
        {
            return Task.FromResult(Result.Success());
        }
    }
    public record CreateCarsRequest(CreateCarsRequestDto Model, ICaller Caller) : IRequest<CreateCarsRequestDto>;
    public record CreateCarsRequestDto : IRequestDto;
}
