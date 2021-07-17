using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar
{
    public class GetCarFeature : QueryFeatureBase<GetCarRequest, GetCarResponse>
    {
        public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
        {
            Car item = new("Renault", 2012, "whatever", "whatever");
            return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
        }
    }

    public record GetCarRequest(ICaller Caller) : IRequest;
    public record GetCarResponse(Car Item) : IGetSingleItemResponse<Car>;
    public record Car(string Model, int Year, string Detail1, string Detail2);
}
