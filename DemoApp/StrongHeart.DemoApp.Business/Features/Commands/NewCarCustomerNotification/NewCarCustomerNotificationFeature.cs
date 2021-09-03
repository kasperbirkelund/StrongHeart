using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.NewCarCustomerNotification
{
    public partial class NewCarCustomerNotificationFeature
    {
        protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
        {
            yield break;
        }

        public override Task<Result> Execute(NewCarCustomerNotificationRequest request)
        {
            //do stuff
            return Task.FromResult(Result.QueuedForLaterExecution());
        }

        protected override IEnumerable<ValidationMessage> ValidateRequest(NewCarCustomerNotificationRequest request)
        {
            yield break;
        }
    }
}