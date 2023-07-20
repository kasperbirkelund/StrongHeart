using System;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.TimeAlert
{
    public class TimeExceededData
    {
        public TimeSpan ActualDuration { get; }
        public IRequest Request { get; }
        public TimeSpan MaxAllowedExecutionTime { get; }

        public TimeExceededData(TimeSpan actualDuration, IRequest request, TimeSpan maxAllowedExecutionTime)
        {
            ActualDuration = actualDuration;
            Request = request;
            MaxAllowedExecutionTime = maxAllowedExecutionTime;
        }
    }
}