using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Core.EventAggregator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBroadCaster(this IServiceCollection services, bool doLogToConsole)
        {
            services.AddSingleton<IEventPublisher>(x => EventBroadcaster.Create(doLogToConsole/*, x.GetRequiredService<ConnectionString>()*/));
            return services;
        }
    }
}