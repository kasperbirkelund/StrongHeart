using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.DemoApp.WebApi.Controllers;
using StrongHeart.Features.DependencyInjection;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;
using StrongHeart.Features.Documentation.Visitors;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.DemoApp.WebApi.Tests
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
            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            var config = configMock.Object;
            Assembly assembly = typeof(CarsController).Assembly;
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IConfiguration>(_ => config);
            new Startup(config).ConfigureServices(services);

            var sourceCodeDir = @"C:\development\azuredevops\StrongHeart\DemoApp\StrongHeart.DemoApp.WebApi";//CodeCommentSection.GetSourceCodeDirFromFeature<CarsController>(@"\DemoApp\");

            MarkDownVisitor visitor = new MarkDownVisitor();
            DocumentationGeneratorUtil.GenerateToVisitor(assembly, services, sourceCodeDir, visitor, x=> x.DocName == DocumentationConstants.Setup);
            _helper.WriteLine(visitor.AsString());
        }
    }
}
