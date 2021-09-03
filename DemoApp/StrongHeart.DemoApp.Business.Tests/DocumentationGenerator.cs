﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.Features.Core.Events;
using StrongHeart.Features.DependencyInjection;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Visitors;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.DemoApp.Business.Tests
{
    public class DummyEventPublisher : IEventPublisher
    {
        public Task Publish<T>(T evt) where T : class, IEvent
        {
            throw new NotImplementedException();
        }
    }

    public class DocumentationGenerator
    {
        private readonly ITestOutputHelper _helper;

        public DocumentationGenerator(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        [Fact]
        public void GenerateDocumentation()
        {
            Assembly assembly = typeof(FeatureBase).Assembly;
            IServiceCollection services = new ServiceCollection();
            services.AddStrongHeart(_ => { }, null, assembly);
            services.AddTransient<IFoo, Foo>();
            services.AddSingleton<IEventPublisher, DummyEventPublisher>();
            Type[] features = typeof(FeatureBase).Assembly.GetExportedTypes()
                .Where(x => typeof(IDocumentationDescriber).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass)
                .ToArray();
            Array.ForEach(features, type => services.AddTransient(type));

            string sourceCodeDir = CodeCommentSection.GetSourceCodeDirFromFeature<GetCarFeature>(@"\DemoApp\");

            MarkDownVisitor visitor = new MarkDownVisitor();
            DocumentationGeneratorUtil.GenerateToVisitor(assembly, services, sourceCodeDir, visitor);
            _helper.WriteLine(visitor.AsString());
        }
    }
}
