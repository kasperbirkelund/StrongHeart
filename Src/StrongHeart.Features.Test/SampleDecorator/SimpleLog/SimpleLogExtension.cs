using System;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog;

public class SimpleLogExtension<TSimpleLog> : IPipelineExtension
    where TSimpleLog : class, ISimpleLog
{
    public Func<Type, bool> ShouldApplyPipelineExtension => _ => true; //always apply this extension
    public Type QueryTypeDecorator => typeof(SimpleLogQueryDecorator<,>);
    public Type CommandTypeDecorator => typeof(SimpleLogCommandDecorator<,>);
    public Type EventHandlerDecorator => throw new NotSupportedException("Not supported.");
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ISimpleLog, TSimpleLog>();
    }
}