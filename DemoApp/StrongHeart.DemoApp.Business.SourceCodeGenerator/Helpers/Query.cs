using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    internal class Query
    {
        public static string GetGeneratedCode(IEnumerable<AdditionalText> additionalFiles)
        {
            QueryFeatures features = GetQueryFeatures(additionalFiles);
            StringBuilder sb = new();
            foreach (QueryFeature feature in features.Items)
            {
                sb.AppendLine(CreateCodeSnippet(feature, features.RootNamespace));
            }

            return sb.ToString();
        }

        private static QueryFeatures GetQueryFeatures(IEnumerable<AdditionalText> additionalFiles)
        {
            SourceText? queries = additionalFiles
                    .Where(x => x.Path.EndsWith("queries.xml"))
                    .Select(x => x.GetText())
                    .SingleOrDefault();

            XmlSerializer serializer = new XmlSerializer(typeof(QueryFeatures));

            using (TextReader reader = new StringReader(queries.ToString()))
            {
                QueryFeatures result = (QueryFeatures)serializer.Deserialize(reader);
                return result;
            }
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
            StringBuilder sb = new StringBuilder();
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
