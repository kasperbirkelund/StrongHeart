using System;
using System.Text;
using Microsoft.CodeAnalysis;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers;
using StrongHeart.DemoApp.Business.SourceCodeGenerator.Helpers.Xml;

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
            sb.AppendLine(Query.GetGeneratedCode(new XmlQueryFeatureReader(), context.AdditionalFiles));
            sb.AppendLine(Command.GetGeneratedCode(new XmlCommandFeatureReader(), context.AdditionalFiles));
            
            context.AddSource("Features.generated.cs", sb.ToString());
        }
    }
}
