using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    public class ExceptionLoggerExtension : IPipelineExtension
    {
        private readonly Func<IExceptionLogger> _exceptionLogger;

        public ExceptionLoggerExtension(Func<IExceptionLogger> exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => true; //always apply this extension
        public Type QueryTypeDecorator => typeof(ExceptionLoggerQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(ExceptionLoggerCommandDecorator<,>);
        //public Type EventHandlerTypeDecorator => typeof(ExceptionLoggerEventHandlerDecorator<>);
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IExceptionLogger>(provider => _exceptionLogger());
        }
    }
}