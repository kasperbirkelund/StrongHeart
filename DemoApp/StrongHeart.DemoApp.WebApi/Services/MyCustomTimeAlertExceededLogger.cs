using System.Threading.Tasks;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.DemoApp.WebApi.Services
{
    public class MyCustomTimeAlertExceededLogger : ITimeAlertExceededLogger
    {
        public Task LogTimeExceeded(TimeExceededData data)
        {
            return Task.CompletedTask;
        }
    }
}
