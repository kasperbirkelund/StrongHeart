using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar
{
    public partial class GetCarFeature
    {
        public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
        {
            CarDetails item = new("Renault", 2012, "whatever", "whatever");
            return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
        }
    }
}