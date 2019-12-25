using System.Collections.Generic;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Audit;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Helpers
{
    public class PipelineExtensionsStub : List<IPipelineExtension>
    {
        private readonly FeatureAuditRepositorySpy _auditRepositorySpy = new FeatureAuditRepositorySpy();
        private readonly ExceptionLoggerSpy _exceptionLoggerSpy = new ExceptionLoggerSpy();

        public FeatureAuditRepositorySpy AuditRepoSpy => _auditRepositorySpy;
        public ExceptionLoggerSpy ExceptionLoggerSpy => _exceptionLoggerSpy;
        
        public PipelineExtensionsStub()
        {
            Add(new AuditExtension(() => _auditRepositorySpy));
            Add(new ExceptionLoggerExtension(() => _exceptionLoggerSpy));
            Add(new AuthorizationExtension());
            Add(new RequestValidatorExtension());
            Add(new FilterExtension());
        }
    }
}