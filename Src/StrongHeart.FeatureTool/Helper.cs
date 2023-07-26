using System;
using System.IO;
using System.Threading.Tasks;
using Spectre.Console;

namespace StrongHeart.FeatureTool;

internal static class Helper
{
    public static async Task WriteFileAsync(AddFeatureSettings settings, string featureType, string content)
    {
        string dir = GetRootDir(settings);
        dir = Path.Combine(dir, featureType);
        dir = Path.Combine(dir, settings.FeatureName);

        Directory.CreateDirectory(dir);

        string file = GetFileName(settings);
        string fullPath = Path.Combine(dir, file);

        TextPath renderablePath = new TextPath(fullPath)
            .RootColor(Color.Blue)
            .SeparatorColor(Color.Blue)
            .StemColor(Color.Blue)
            .LeafColor(Color.Green);

        if (File.Exists(fullPath))
        {
            AnsiConsole.Markup("[black on orange1]File already exists and will not be overwritten[/]");
            AnsiConsole.Write(renderablePath);
        }
        else
        {
            await File.WriteAllTextAsync(fullPath, content);
            AnsiConsole.Write("Generated file: ");
            AnsiConsole.Write(renderablePath);
        }
    }

    private static string GetFileName(AddFeatureSettings settings)
    {
        return settings.FeatureName + "Feature.cs";
    }

    public static string GetNamespace(AddFeatureSettings settings, string? featureType)
    {
        string full = $"{settings.ProjectName}.{settings.FeaturesFolder}.{featureType}.{settings.SubfolderName}.{settings.FeatureName}";
        return full.Replace("..", ".");
    }

    public static string GetRootDir(AddFeatureSettings settings)
    {
        if (settings.FeaturesFolder is null)
        {
            return Path.Combine(Environment.CurrentDirectory, settings.ProjectName);
        }
        else
        {
            return Path.Combine(Environment.CurrentDirectory, $"{settings.ProjectName}/{settings.FeaturesFolder}");
        }
    }
}