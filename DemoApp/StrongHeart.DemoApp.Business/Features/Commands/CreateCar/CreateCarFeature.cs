using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.DemoApp.Business.Features.Commands.CreateCar
{
    public class CreateCarFeature : CommandFeatureBase<CreateCarRequest, CreateCarDto>, IRequestValidatable<CreateCarRequest>
    {
        public override Task<Result> Execute(CreateCarRequest request)
        {
            //Put the request on a queue and return "QueuedForLaterExecution" -
            //OR do the job immediately and return Result.Success()
            return Task.FromResult(Result.QueuedForLaterExecution());
        }
        public Func<CreateCarRequest, IEnumerable<ValidationMessage>> ValidationFunc() => ValidateRequest;

        private IEnumerable<ValidationMessage> ValidateRequest(CreateCarRequest request)
        {
            //Just any insane validation
            if (request.Model.Model != "Skoda")
            {
                yield return "Model must be Skoda";
            }
        }
    }

    public record CreateCarRequest(Guid Id, CreateCarDto Model, ICaller Caller) : IRequest<CreateCarDto>;
    public record CreateCarDto(string Model) : IRequestDto;
}