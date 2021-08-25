using System.Text;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    internal class QueryFeatureCodeGenerator
    {
        public static string GetGeneratedCode(IFeatureReader<QueryFeatures> reader)
        {
            QueryFeatures features = reader.GetFeatures();
            StringBuilder sb = new();
            foreach (QueryFeature feature in features.Items)
            {
                sb.AppendLine(CreateCodeSnippet(feature, features.RootNamespace));
            }

            return sb.ToString();
        }

        private static string CreateCodeSnippet(QueryFeature feature, string rootNamespace)
        {
            string template = $@"namespace {rootNamespace}.Queries.{feature.Name}
{{
{GetUsings(feature.Response)}

    public partial class {feature.Name}Feature : QueryFeatureBase<{feature.Name}Request, {feature.Name}Response>
    {{
    }}

    public record {feature.Name}Request({GetRequestParameters(feature.Request)}) : IRequest;
    public record {feature.Name}Response({GetResponseParameters(feature.Response)}) : {GetResponseInterface(feature.Response)};
    public record {feature.Response.ResponseTypeName}({string.Join(", ", feature.Response.Properties)});
}}
";
            return template;
        }

        private static string GetUsings(QueryResponse response)
        {
            StringBuilder sb = new();
            if (response.IsListResponse)
            {
                sb.AppendLine("\tusing System.Collections.Generic;");
            }
            sb.AppendLine("\tusing System.Threading.Tasks;");
            sb.AppendLine("\tusing StrongHeart.Core.Security;");
            sb.AppendLine("\tusing StrongHeart.DemoApp.Business.Features;");
            sb.Append("\tusing StrongHeart.Features.Core;");

            return sb.ToString();
        }

        private static string GetResponseParameters(QueryResponse response)
        {
            return response.IsListResponse ?
                $"ICollection<{response.ResponseTypeName}> Items" :
                $"{response.ResponseTypeName} Item";
        }

        private static string GetRequestParameters(QueryRequest request)
        {
            request.Properties.Add("ICaller Caller");
            return string.Join(", ", request.Properties);
        }

        private static string GetResponseInterface(QueryResponse response)
        {
            return response.IsListResponse ?
                $"IGetListResponse<{response.ResponseTypeName}>" :
                $"IGetSingleItemResponse<{response.ResponseTypeName}>";
        }
    }
}
