namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;

public interface IFeatureReader<out T>
{
    T GetFeatures();
}