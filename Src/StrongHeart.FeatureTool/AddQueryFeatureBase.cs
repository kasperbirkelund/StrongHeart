using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public abstract class AddQueryFeatureBase : AsyncCommand<AddFeatureSettings>
{
    protected static string GetQuery(string project, string queryName, bool isList)
    {
        string s = $@"using System.Threading.Tasks;
using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {project}.Features.Queries.{queryName}
{{
    {Helper.GetGeneratedCodeText()}
    public record {queryName}Request(ICaller Caller) : IRequest;
    {Helper.GetGeneratedCodeText()}
    public record {queryName}();
    {GetResponseClass(queryName, isList)}

    {Helper.GetGeneratedCodeText()}
    public partial class {queryName}Feature : IQueryFeature<{queryName}Request, {queryName}Response>
    {{
        public Task<Result<{queryName}Response>> Execute({queryName}Request request)
        {{
            {GetResponseContent(queryName, isList)}
            {queryName}Response response = new(item{(isList ? "s" : string.Empty)});
            Result<{queryName}Response> result = Result<{queryName}Response>.Success(response);
            return Task.FromResult(result);
        }}
    }}
}}";
        return s;
    }

    private static string GetResponseContent(string queryName, bool isList)
    {
        if (isList)
        {
            return $"List<{queryName}> items = new();";
        }
        else
        {
            return $"{queryName} item = new();";
        }
    }

    private static string GetResponseClass(string queryName, bool isList)
    {
        if (isList)
        {
            return @$"{Helper.GetGeneratedCodeText()}
public class {queryName}Response : IGetListResponse<{queryName}>
    {{
        public {queryName}Response(ICollection<{queryName}> items)
        {{
            Items = items;
        }}


        public ICollection<{queryName}> Items {{ get; }}
    }}";
        }
        else
        {
            return $@"{Helper.GetGeneratedCodeText()}
    public class {queryName}Response : IGetSingleItemResponse<{queryName}> 
    {{
        public {queryName}Response({queryName}? item)
        {{
            Item = item;
        }}

        public {queryName}? Item {{ get; }}
    }}";
        }
    }
}