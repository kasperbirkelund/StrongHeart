using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.TimeAlert;

public class TimeAlertExtension<TTimeAlertExceededLogger> : IPipelineExtension
    where TTimeAlertExceededLogger : class, ITimeAlertExceededLogger
{
    public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(ITimeAlert));
    public Type QueryTypeDecorator => typeof(TimeAlertQueryDecorator<,>);
    public Type CommandTypeDecorator => typeof(TimeAlertCommandDecorator<,>);
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ITimeAlertExceededLogger, TTimeAlertExceededLogger>();
    }
}