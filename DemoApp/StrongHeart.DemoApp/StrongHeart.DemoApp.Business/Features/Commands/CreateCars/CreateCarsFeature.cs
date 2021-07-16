using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.DemoApp.Business.Features.Commands.CreateCars
{
    public class CreateCarsFeature : CommandFeatureBase<CreateCarsRequest, CreateCarsRequestDto>, IRequestValidatable<CreateCarsRequest>
    {
        public override Task<Result> Execute(CreateCarsRequest request)
        {
            //Put the request on a queue and return "QueuedForLaterExecution" or do the job now and return Result.Success()
            return Task.FromResult(Result.QueuedForLaterExecution());
        }
        public Func<CreateCarsRequest, IEnumerable<ValidationMessage>> ValidationFunc() => ValidateRequest;

        private IEnumerable<ValidationMessage> ValidateRequest(CreateCarsRequest request)
        {
            //Just any insane validation
            if (request.Model.Model != "Skoda")
            {
                yield return "Model must be Skoda";
            }
        }
    }

    public record CreateCarsRequest(Guid Id, CreateCarsRequestDto Model, ICaller Caller) : IRequest<CreateCarsRequestDto>;
    public record CreateCarsRequestDto(string Model) : IRequestDto;
}
