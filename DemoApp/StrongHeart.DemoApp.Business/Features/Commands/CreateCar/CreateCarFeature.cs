using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.CreateCar
{
    public partial class CreateCarFeature
    {
        public override Task<Result> Execute(CreateCarRequest request)
        {
            //Put the request on a queue and return "QueuedForLaterExecution" -
            //OR do the job immediately and return Result.Success()
            return Task.FromResult(Result.QueuedForLaterExecution());
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(CreateCarRequest request)
        {
            //Just any insane validation
            if (request.Model.Model != "Skoda")
            {
                yield return "Model must be Skoda";
            }
        }

        protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
        {
            yield break;
        }
    }
}