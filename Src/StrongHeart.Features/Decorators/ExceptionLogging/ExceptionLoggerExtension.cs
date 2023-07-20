using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    public class ExceptionLoggerExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => _ => true; //always apply this extension
        public Type QueryTypeDecorator => typeof(ExceptionLoggerQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(ExceptionLoggerCommandDecorator<,>);
        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}