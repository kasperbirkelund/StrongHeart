using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar
{
    public partial class GetCarFeature
    {
        public GetCarFeature()
        {
            
        }
        public GetCarFeature(int i)
        {

        }

        public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
        {
            CarDetails item = new("Renault", 2012, "whatever", "whatever");
            return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
        }
    }
}