using StrongHeart.Core.FeatureToggling;
using StrongHeart.DemoApp.WebApi.Services;

namespace StrongHeart.DemoApp.WebApi.Toggles;

public class MyToggle : IFeatureToggle<MyToggle>
{
    private bool? _isEnabled = null;
    private readonly IConfigurationReader _foo;

    public MyToggle(IConfigurationReader foo)
    {
        //Read from database, environment variable,
        //file, configuration or whatever you need as decision point
        _foo = foo;
    }

    public bool IsFeatureEnabled
    {
        get
        {
            _isEnabled ??= _foo.GetValue();
            return _isEnabled.Value;
        }
    }
}