﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.Features.DependencyInjection;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Visitors;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.DemoApp.Business.Tests
{
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
            IServiceCollection services = new ServiceCollection();
            services.AddStrongHeart(_ => { }, typeof(FeatureBase).Assembly);
            services.AddTransient<IFoo, Foo>();
            Type[] features = typeof(FeatureBase).Assembly.GetExportedTypes()
                .Where(x => typeof(IDocumentationDescriber).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass)
                .ToArray();
            Array.ForEach(features, type => services.AddTransient(type));

            IServiceProvider provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var items = features
                    .Select(x => scope.ServiceProvider.GetRequiredService(x))
                    .OfType<IDocumentationDescriber>();

                foreach (IDocumentationDescriber item in items)
                {
                    var visitor = new MarkDownVisitor();
                    IEnumerable<ISection> sections = item!.GetDocumentationSections(new DummyCaller());
                    visitor.Accept(sections);
                    _helper.WriteLine(visitor.AsString());
                }
            }
        }

        internal class DummyCaller : ICaller
        {
            public Guid Id => Guid.Empty;

            public IReadOnlyList<Claim> Claims => new Collection<Claim>();
        }
    }
}
