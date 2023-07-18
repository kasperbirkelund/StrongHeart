using System;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddQuerySingleFeature : AddQueryFeatureBase
{
    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
    {
        await Console.Out.WriteLineAsync(GetQuery(settings.ProjectName, settings.FeatureName, false));
        return 0;
    }
}