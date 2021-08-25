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
            context.RegisterForPostInitialization(x =>
            {
                StringBuilder sb = new();
                sb.AppendLine($"//Generated: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");
                sb.AppendLine("#nullable enable");
                sb.AppendLine(QueryFeatureCodeGenerator.GetGeneratedCode(new YamlQueryFeatureReader(new StringReader(Q))));
                sb.AppendLine(CommandFeatureCodeGenerator.GetGeneratedCode(new YamlCommandFeatureReader(new StringReader(C))));

                x.AddSource("Features.generated.cs", sb.ToString());
            });
        }

        public const string C = @"- rootNameSpace: StrongHeart.DemoApp.Business.Features
- name: CreateCar
  additionalRequestProperties:
    - Guid Id
  dtoProperties:
    - string Model

- name: UpdateCar
  additionalRequestProperties:    
  dtoProperties:
    - Guid Id
    - string Model

- name: DeleteCar
  additionalRequestProperties:    
  dtoProperties:
    - Guid Id";

        public const string Q = @"- rootNameSpace: StrongHeart.DemoApp.Business.Features
- name: GetCar
  requestProperties:
  responseTypeName: CarDetails
  isListResponse: false
  responseProperties:
    - string Model
    - int Year
    - string Detail1
    - string Detail2    

- name: GetCars
  requestProperties:
    - string? Model    
  responseTypeName: Car
  isListResponse: true
  responseProperties:
    - string Model
    - int Year";

        public void Execute(GeneratorExecutionContext context)
        {
            //NO OP
        }
    }
}
