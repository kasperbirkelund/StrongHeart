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

        await File.WriteAllTextAsync(fullPath, content);

        TextPath renderablePath = new TextPath(fullPath)
            .RootColor(Color.Blue)
            .SeparatorColor(Color.Blue)
            .StemColor(Color.Blue)
            .LeafColor(Color.Green);

        AnsiConsole.Write("Generated file: ");
        AnsiConsole.Write(renderablePath);
    }

    private static string GetFileName(AddFeatureSettings settings)
    {
        return settings.FeatureName + "Feature.cs";
    }

    //public static string? GetGeneratedCodeText(bool generatePartialClasses)
    //{
    //    return generatePartialClasses ? $"[System.CodeDom.Compiler.GeneratedCode(\"StrongHeart.FeatureTool\", \"{typeof(Helper).Assembly.GetName().Version}\")]" + Environment.NewLine : null;
    //}

    public static string GetNamespace(AddFeatureSettings settings)
    {
        return settings.SubfolderName is null ? settings.ProjectName : $"{settings.ProjectName}.{settings.SubfolderName}";
    }

    public static string GetRootDir(AddFeatureSettings settings)
    {
        if (settings.SubfolderName is null)
        {
            return Path.Combine(Environment.CurrentDirectory, settings.ProjectName);
        }
        else
        {
            return Path.Combine(Environment.CurrentDirectory, $"{settings.ProjectName}/{settings.SubfolderName}");
        }
    }
}