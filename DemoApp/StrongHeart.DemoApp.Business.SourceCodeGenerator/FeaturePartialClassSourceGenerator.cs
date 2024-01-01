using System;
using System.Text;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Yaml;

namespace StrongHeart.DemoApp.Business.SourceCodeGenerator;

[Generator]
public class FeaturePartialClassSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization(x =>
        {
            StringBuilder sb = new();
            sb.AppendLine($"//Generated: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");
            sb.AppendLine("#nullable enable");
            sb.AppendLine(QueryFeatureCodeGenerator.GetGeneratedCode(new YamlQueryFeatureReader(new EmbeddedResourceReader("queries.yaml"))));
            sb.AppendLine(CommandFeatureCodeGenerator.GetGeneratedCode(new YamlCommandFeatureReader(new EmbeddedResourceReader("commands.yaml"))));

            x.AddSource("Features.generated.cs", sb.ToString());
        });
    }

    public void Execute(GeneratorExecutionContext context)
    {
        //NO OP
    }
}