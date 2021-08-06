using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation
{
    public static class DocumentationGeneratorUtil
    {
        public static void GenerateToVisitor(Assembly assembly, IServiceCollection services, string sourceCodeDir, ISectionVisitor visitor)
        {
            GenerateToVisitor(assembly, services, sourceCodeDir, visitor, _ => true);
        }
        public static void GenerateToVisitor(Assembly assembly, IServiceCollection services, string sourceCodeDir, ISectionVisitor visitor, Func<IDocumentationDescriber, bool> predicate)
        {
            CodeCommentSection.SourceCodeDir = sourceCodeDir;

            Type[] types = assembly.GetIDocumentationDescriberTypes();
            Array.ForEach(types, type => services.AddTransient(type));

            IServiceProvider provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                IEnumerable<IDocumentationDescriber> items = types
                    .GetInstances(scope.ServiceProvider)
                    .GetCorrectItems(predicate);

                foreach (IDocumentationDescriber item in items)
                {
                    IEnumerable<ISection> sections = item!.GetDocumentationSections(new DocumentationGenerationContext());
                    visitor.Accept(sections);
                }
            }
        }

        private static Type[] GetIDocumentationDescriberTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(x => typeof(IDocumentationDescriber).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass)
                .ToArray();
        }

        private static IEnumerable<IDocumentationDescriber> GetInstances(this IEnumerable<Type> items, IServiceProvider provider)
        {
            return items.Select(provider.GetRequiredService).Cast<IDocumentationDescriber>(); ;
        }

        private static IEnumerable<IDocumentationDescriber> GetCorrectItems(this IEnumerable<IDocumentationDescriber> items, Func<IDocumentationDescriber, bool> predicate)
        {
            return items.Where(predicate)
                .OrderBy(x => x.DocName)
                .ThenBy(x => x.Order);
        }
    }
}
