using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.Features.Test.Helpers;

public class TimeAlertExceededLoggerSpy : ITimeAlertExceededLogger
{
    public readonly IList<TimeExceededData> Data = new List<TimeExceededData>();
    public Task LogTimeExceeded(TimeExceededData data)
    {
        Data.Add(data);
        return Task.CompletedTask;
    }
}