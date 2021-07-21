using System;
using System.Text;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;

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
            sb.AppendLine($"//Generated: {DateTime.Now.ToLongTimeString()}");
            sb.AppendLine(Query.GetGeneratedCode(context.AdditionalFiles));
            sb.AppendLine(Command.GetGeneratedCode(context.AdditionalFiles));
            context.AddSource("Features.generated.cs", sb.ToString());
        }
    }
}
