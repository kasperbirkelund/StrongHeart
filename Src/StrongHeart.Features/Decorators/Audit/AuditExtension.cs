using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.Audit
{
    public class AuditExtension : IPipelineExtension
    {
        private readonly Func<IFeatureAuditRepository>  _repository;

        public AuditExtension(Func<IFeatureAuditRepository> repository)
        {
            _repository = repository;
        }

        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IAuditable<>));
        public Type QueryTypeDecorator => typeof(AuditLoggingQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(AuditLoggingCommandDecorator<,>);
        public Type EventHandlerTypeDecorator => typeof(AuditLoggingEventHandlerDecorator<>);
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IFeatureAuditRepository>(provider => _repository());
        }
    }
}