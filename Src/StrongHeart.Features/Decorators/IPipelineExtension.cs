using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators;

public interface IPipelineExtension
{
    Func<Type, bool> ShouldApplyPipelineExtension { get; }
    Type QueryTypeDecorator { get; }
    Type CommandTypeDecorator { get; }
    void RegisterServices(IServiceCollection services);
}