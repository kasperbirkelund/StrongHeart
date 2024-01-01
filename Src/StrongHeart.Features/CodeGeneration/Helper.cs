using System;
using System.IO;

namespace StrongHeart.Features.CodeGeneration;

internal class Helper
{
    //public static string GetFile(AddFeatureSettings settings, string featureType)
    //{
    //    string dir = GetRootDir(settings);
    //    dir = Path.Combine(dir, featureType);
    //    dir = Path.Combine(dir, settings.FeatureName);

    //    Directory.CreateDirectory(dir);

    //    string file = GetFileName(settings);
    //    string fullPath = Path.Combine(dir, file);
    //}

    private static string GetFileName(AddFeatureSettings settings)
    {
        return settings.FeatureName + "Feature.cs";
    }

    public static string GetNamespace(AddFeatureSettings settings, string? featureType)
    {
        string full = $"{settings.ProjectName}.{settings.FeaturesFolder}.{featureType}.{settings.SubCategoryName}.{settings.FeatureName}";
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