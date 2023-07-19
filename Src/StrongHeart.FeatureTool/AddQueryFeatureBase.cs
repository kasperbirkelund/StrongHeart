using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public abstract class AddQueryFeatureBase : AsyncCommand<AddFeatureSettings>
{
    protected static string GetQuery(AddFeatureSettings settings, bool isList)
    {
        string s = $@"using System.Threading.Tasks;
using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {Helper.GetNamespace(settings)}.Queries.{settings.FeatureName}
{{
    public record {settings.FeatureName}Request(ICaller Caller) : IRequest;
    
    //TODO: Verify that the name of this record is good
    public record {settings.FeatureName}();
    {GetResponseClass(settings, isList)}

    public class {settings.FeatureName}Feature : {settings.FeatureBaseType}<{settings.FeatureName}Request, {settings.FeatureName}Response>
    {{
        public override Task<Result<{settings.FeatureName}Response>> Execute({settings.FeatureName}Request request)
        {{
            {GetResponseContent(settings.FeatureName, isList)}
            {settings.FeatureName}Response response = new(item{(isList ? "s" : string.Empty)});
            Result<{settings.FeatureName}Response> result = Result<{settings.FeatureName}Response>.Success(response);
            return Task.FromResult(result);
        }}
    }}
}}";
        return s;
    }

    private static string GetResponseContent(string featureName, bool isList)
    {
        if (isList)
        {
            return $"List<{featureName}> items = new();";
        }
        else
        {
            return $"{featureName} item = new();";
        }
    }

    private static string GetResponseClass(AddFeatureSettings settings, bool isList)
    {
        if (isList)
        {
            return @$"
    public class {settings.FeatureName}Response : IGetListResponse<{settings.FeatureName}>
    {{
        public {settings.FeatureName}Response(ICollection<{settings.FeatureName}> items)
        {{
            Items = items;
        }}


        public ICollection<{settings.FeatureName}> Items {{ get; }}
    }}";
        }
        else
        {
            return $@"
    public class {settings.FeatureName}Response : IGetSingleItemResponse<{settings.FeatureName}> 
    {{
        public {settings.FeatureName}Response({settings.FeatureName}? item)
        {{
            Item = item;
        }}

        public {settings.FeatureName}? Item {{ get; }}
    }}";
        }
    }
}