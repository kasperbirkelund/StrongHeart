using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.ExceptionLogging;

public class ExceptionLoggerExtension<TExceptionLogger> : IPipelineExtension
    where TExceptionLogger : class, IExceptionLogger
{
    public Func<Type, bool> ShouldApplyPipelineExtension => _ => true; //always apply this extension
    public Type QueryTypeDecorator => typeof(ExceptionLoggerQueryDecorator<,>);
    public Type CommandTypeDecorator => typeof(ExceptionLoggerCommandDecorator<,>);
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IExceptionLogger, TExceptionLogger>();
    }
}