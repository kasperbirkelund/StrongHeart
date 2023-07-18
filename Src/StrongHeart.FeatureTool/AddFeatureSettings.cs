using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddFeatureSettings : CommandSettings
{
    [CommandOption("--subfolder-name")]
    [CommandArgument(0, "<SubfolderName>")]
    public string? SubfolderName { get; set; }

    [CommandOption("--project-name")]
    [CommandArgument(0, "<ProjectName>")]
    public string ProjectName { get; set; }

    [CommandOption("--feature-name")]
    [CommandArgument(0, "<FeatureName>")]
    public string FeatureName { get; set; }
}