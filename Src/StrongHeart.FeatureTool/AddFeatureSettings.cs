using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddFeatureSettings : CommandSettings
{
    [CommandOption("--project-name")]
    [CommandArgument(0, "<ProjectName>")]
    public string ProjectName { get; set; }

    [CommandOption("--generate-partial-file")]
    [CommandArgument(0, "<GeneratePartialFile>")]
    public bool GeneratePartialFile { get; set; }

    [CommandOption("--subfolder-name")]
    [CommandArgument(0, "<SubfolderName>")]
    public string SubfolderName { get; set; }

    [CommandOption("--feature-name")]
    [CommandArgument(0, "<FeatureName>")]
    public string FeatureName { get; set; }
}