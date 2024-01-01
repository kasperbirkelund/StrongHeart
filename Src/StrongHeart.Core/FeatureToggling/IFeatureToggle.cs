namespace StrongHeart.Core.FeatureToggling;

public interface IFeatureToggle<T> where T : IFeatureToggle<T>
{
    bool IsFeatureEnabled { get; }
}