namespace StrongHeart.Features.Decorators.TimeAlert;

public interface ITimeAlert
{
    TimeSpan MaxAllowedExecutionTime { get; }
}