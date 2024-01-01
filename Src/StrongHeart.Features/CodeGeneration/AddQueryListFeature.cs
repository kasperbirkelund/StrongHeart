using System.Threading.Tasks;

namespace StrongHeart.Features.CodeGeneration;

//public class AddQueryListFeature : AddQueryFeatureBase
//{
//    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
//    {
//        string content = GetQuery(settings, true);
//        await Helper.WriteFileAsync(settings, "Queries", content);
//        return 0;
//    }
//}