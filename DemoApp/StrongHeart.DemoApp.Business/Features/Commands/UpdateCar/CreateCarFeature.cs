using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.DemoApp.Business.Features.Commands.UpdateCar
{
    public partial class UpdateCarFeature
    {
        public override Task<Result> Execute(UpdateCarRequest request)
        {
            //Do work
            return Task.FromResult(Result.QueuedForLaterExecution());
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(UpdateCarRequest request)
        {
            if (request.Model.Model != "Skoda")
            {
                yield return "Model must be Skoda";
            }
        }
    }
}