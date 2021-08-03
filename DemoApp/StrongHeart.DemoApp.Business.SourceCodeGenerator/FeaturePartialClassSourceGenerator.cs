using System;
using System.Text;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Yaml;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator
{
    [Generator]
    public class FeaturePartialClassSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sb = new();
            sb.AppendLine($"//Generated: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");
            sb.AppendLine("#nullable enable");
            sb.AppendLine(QueryFeatureCodeGenerator.GetGeneratedCode(new YamlQueryFeatureReader(), context.AdditionalFiles));
            sb.AppendLine(CommandFeatureCodeGenerator.GetGeneratedCode(new YamlCommandFeatureReader(), context.AdditionalFiles));
            
            context.AddSource("Features.generated.cs", sb.ToString());
        }
    }
}
