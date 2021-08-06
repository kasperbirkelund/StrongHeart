using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Dto;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers
{
    internal class CommandFeatureCodeGenerator
    {
        public static string GetGeneratedCode(IFeatureReader<CommandFeatures> reader, IEnumerable<AdditionalText> additionalFiles)
        {
            CommandFeatures features = reader.GetFeatures(additionalFiles);
            StringBuilder sb = new();
            foreach (CommandFeature feature in features.Items)
            {
                sb.AppendLine(CreateCodeSnippet(feature, features.RootNamespace));
            }

            return sb.ToString();
        }

        private static string CreateCodeSnippet(CommandFeature feature, string rootNamespace)
        {
            string template = $@"
namespace {rootNamespace}.Commands.{feature.Name}
{{
{GetUsings()}
    public partial class {feature.Name}Feature : CommandFeatureBase<{feature.Name}Request, {GetDtoName(feature.Name)}>
    {{        
    }}

    public record {feature.Name}Request({GetRequestParameters(feature)}) : IRequest<{GetDtoName(feature.Name)}>;
    public record {GetDtoName(feature.Name)}({GetDtoParameters(feature)}) : IRequestDto;
}}";
            return template;
        }

        private static string GetDtoName(string featureName)
        {
            return featureName + "Dto";
        }

        private static string GetDtoParameters(CommandFeature feature)
        {
            return $"{string.Join(", ", feature.Request.DtoProperties)}";
        }

        private static string GetUsings()
        {
            StringBuilder sb = new();
            sb.AppendLine("\tusing System;");
            sb.AppendLine("\tusing System.Collections.Generic;");
            sb.AppendLine("\tusing System.Threading.Tasks;");
            sb.AppendLine("\tusing StrongHeart.Core.Security;");
            sb.AppendLine("\tusing StrongHeart.Features.Core;");
            sb.AppendLine("\tusing StrongHeart.Features.Decorators.RequestValidation;");
            sb.AppendLine("\tusing StrongHeart.DemoApp.Business.Features;");
            return sb.ToString();
        }
       
        private static string GetRequestParameters(CommandFeature feature)
        {
            feature.Request.AdditionalRequestProperties.Add($"{GetDtoName(feature.Name)} Model");
            feature.Request.AdditionalRequestProperties.Add("ICaller Caller");
            return string.Join(", ", feature.Request.AdditionalRequestProperties);
        }
    }
}
