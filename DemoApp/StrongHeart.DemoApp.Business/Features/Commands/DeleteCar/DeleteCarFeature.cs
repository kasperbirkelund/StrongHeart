using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.DemoApp.Business.Features.Commands.DeleteCar
{
    public partial class DeleteCarFeature
    {
        public override Task<Result> Execute(DeleteCarRequest request)
        {
            //Do work
            return Task.FromResult(Result.Success());
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(DeleteCarRequest request)
        {
            yield break;
        }
    }
}