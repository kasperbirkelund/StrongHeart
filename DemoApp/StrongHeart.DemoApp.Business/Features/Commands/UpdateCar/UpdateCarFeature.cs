using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.UpdateCar
{
    public partial class UpdateCarFeature
    {
        private readonly string[] _validModelNames = new[] {"Skoda"};

        public override Task<Result> Execute(UpdateCarRequest request)
        {
            //Do work
            return Task.FromResult(Result.QueuedForLaterExecution());
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(UpdateCarRequest request)
        {
            if (!_validModelNames.Contains(request.Model.Model))
            {
                yield return "Model must be " + string.Join(", ", _validModelNames);
            }
        }

        protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new TextSection("Valid request values: " + string.Join(", ", _validModelNames));
        }
    }
}