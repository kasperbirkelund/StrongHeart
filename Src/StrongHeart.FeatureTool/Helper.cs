using System;
using System.IO;
using System.Threading.Tasks;
using Spectre.Console;
using StrongHeart.Features.CodeGeneration;

namespace StrongHeart.FeatureTool;

internal static class Helper
{
    public static Task WriteFileAsync(GeneratedCode code)
    {
        return Task.CompletedTask;
        //string dir = GetRootDir(settings);
        //dir = Path.Combine(dir, featureType);
        //dir = Path.Combine(dir, settings.FeatureName);

        //Directory.CreateDirectory(dir);

        //string file = GetFileName(settings);
        //string fullPath = Path.Combine(dir, file);

        //TextPath renderablePath = new TextPath(fullPath)
        //    .RootColor(Color.Blue)
        //    .SeparatorColor(Color.Blue)
        //    .StemColor(Color.Blue)
        //    .LeafColor(Color.Green);

        //if (File.Exists(fullPath))
        //{
        //    AnsiConsole.Markup("[black on orange1]File already exists and will not be overwritten[/]");
        //    AnsiConsole.Write(renderablePath);
        //}
        //else
        //{
        //    await File.WriteAllTextAsync(fullPath, content);
        //    AnsiConsole.Write("Generated file: ");
        //    AnsiConsole.Write(renderablePath);
        //}
    }
}