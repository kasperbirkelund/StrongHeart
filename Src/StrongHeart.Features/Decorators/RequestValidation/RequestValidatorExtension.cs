using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public class RequestValidatorExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IRequestValidator<>));
        public Type QueryTypeDecorator => typeof(RequestValidationQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(RequestValidationCommandDecorator<,>);
        public void RegisterServices(IServiceCollection services)
        {
            //NO OP
        }
    }
}