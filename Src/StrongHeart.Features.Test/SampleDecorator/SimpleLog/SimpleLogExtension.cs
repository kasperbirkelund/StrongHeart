using System;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog
{
    public class SimpleLogExtension : IPipelineExtension
    {
        private readonly Func<ISimpleLog> _simpleLog;

        public SimpleLogExtension(Func<ISimpleLog> simpleLog)
        {
            _simpleLog = simpleLog;
        }
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => true; //always apply this extension
        public Type QueryTypeDecorator => typeof(SimpleLogQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(SimpleLogCommandDecorator<,>);
        //public Type EventHandlerTypeDecorator => typeof(SimpleLogEventHandlerDecorator<>);
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ISimpleLog>(provider => _simpleLog());
        }
    }
}