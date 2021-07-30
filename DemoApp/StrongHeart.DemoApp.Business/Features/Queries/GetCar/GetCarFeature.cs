using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Features.Queries.GetCar
{
    public partial class GetCarFeature
    {
        private readonly IFoo _foo;

        public GetCarFeature(IFoo foo)
        {
            _foo = foo;
        }

        public override Task<Result<GetCarResponse>> Execute(GetCarRequest request)
        {
            //_foo.DoWork(request.Caller);
            CarDetails item = new("Renault", 2012, "whatever", "whatever");
            return Task.FromResult(Result<GetCarResponse>.Success(new GetCarResponse(item)));
        }
    }
}