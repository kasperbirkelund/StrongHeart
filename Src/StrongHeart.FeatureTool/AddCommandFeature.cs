using System.Threading.Tasks;
using Spectre.Console.Cli;
using StrongHeart.Features;

namespace StrongHeart.FeatureTool;

public class AddCommandFeature : AsyncCommand<AddFeatureSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
    {
        string content = GetCommand(settings);
        await Helper.WriteFileAsync(settings, "Commands", content);
        return 0;
    }

    private static string GetCommand(AddFeatureSettings settings)
    {

    }
}