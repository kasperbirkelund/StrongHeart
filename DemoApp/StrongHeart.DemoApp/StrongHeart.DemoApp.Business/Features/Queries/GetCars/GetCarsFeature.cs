using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCars
{
    public class GetCarsFeature : QueryFeatureBase<GetCarsRequest, GetCarsResponse>
    {
        public override Task<Result<GetCarsResponse>> Execute(GetCarsRequest request)
        {
            List<Car> items = new()
            {
                new Car("Toyota")
            };
            return Task.FromResult(Result<GetCarsResponse>.Success(new GetCarsResponse(items)));
        }
    }

    public record GetCarsRequest(ICaller Caller) : IRequest;
    public record GetCarsResponse(ICollection<Car> Items) : IGetListResponse<Car>;
    public record Car(string Model);
}
