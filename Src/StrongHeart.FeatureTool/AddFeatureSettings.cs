using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddFeatureSettings : CommandSettings
{
    [CommandOption("--subfolder-name")]
    [CommandArgument(0, "<SubfolderName>")]
    public string? SubfolderName { get; set; }

    [CommandOption("--features-folder")]
    [CommandArgument(0, "<FeaturesFolder>")]
    public string? FeaturesFolder { get; set; }

    [CommandOption("--project-name")]
    [CommandArgument(0, "<ProjectName>")]
    public string ProjectName { get; set; }

    [CommandOption("--feature-name")]
    [CommandArgument(0, "<FeatureName>")]
    public string FeatureName { get; set; }

    [CommandOption("--feature-base-type")]
    [CommandArgument(0, "<FeatureBaseType>")]
    public string FeatureBaseType { get; set; }
}