using System;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.TimeAlert;

public class TimeExceededData
{
    public TimeSpan ActualDuration { get; }
    public IRequest Request { get; }

    public TimeExceededData(TimeSpan actualDuration, IRequest request)
    {
        ActualDuration = actualDuration;
        Request = request;
    }
}