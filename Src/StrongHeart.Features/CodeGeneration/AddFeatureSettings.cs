namespace StrongHeart.Features.CodeGeneration;

public class AddFeatureSettings
{
    public AddFeatureSettings(string projectName, string featureName, string featureBaseType)
    {
        ProjectName = projectName;
        FeatureName = featureName;
        FeatureBaseType = featureBaseType;
    }

    public string? SubCategoryName { get; set; }
    public string? FeaturesFolder { get; set; }

    public string ProjectName { get; set; }
    public string FeatureName { get; set; }
    public string FeatureBaseType { get; set; }
}