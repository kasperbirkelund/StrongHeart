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
            //DOC-START Special customer rule...
            bool isCustomer = IsCustomer();
            if (isCustomer)
            {
                SendGift();
            }
            //DOC-END

            return Task.FromResult(Result.QueuedForLaterExecution());
        }

        private bool IsCustomer()
        {
            return true;
        }

        private void SendGift()
        {
            //Do something
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(CreateCarRequest request)
        {
            //Just any insane validation

            //DOC-START Validation rule
            if (request.Model.Model != "Skoda")
            {
                yield return "Model must be Skoda";
            }
            //DOC-END
        }

        protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new CodeCommentSection(GetType());
        }
    }
}