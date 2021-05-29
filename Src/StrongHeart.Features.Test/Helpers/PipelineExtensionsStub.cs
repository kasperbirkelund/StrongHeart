using System.Collections.Generic;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Helpers
{
    public class PipelineExtensionsStub : List<IPipelineExtension>
    {
        //public FeatureAuditRepositorySpy AuditRepoSpy { get; } = new FeatureAuditRepositorySpy();
        public ExceptionLoggerSpy ExceptionLoggerSpy { get; } = new ExceptionLoggerSpy();

        public PipelineExtensionsStub()
        {
            //Add(new AuditExtension(() => AuditRepoSpy));
            Add(new ExceptionLoggerExtension(() => ExceptionLoggerSpy));
            Add(new AuthorizationExtension());
            Add(new RequestValidatorExtension());
            Add(new FilterExtension());
        }
    }
}