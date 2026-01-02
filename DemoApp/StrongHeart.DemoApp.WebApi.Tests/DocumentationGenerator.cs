using Microsoft.Extensions.DependencyInjection;
using StrongHeart.DemoApp.WebApi.Controllers;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Visitors;
using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.DemoApp.WebApi.Tests;

public class DocumentationGenerator
{
    private readonly ITestOutputHelper _helper;

    public DocumentationGenerator(ITestOutputHelper helper)
    {
        _helper = helper;
    }

    [Fact(Skip = "ret")]
    public void GenerateDocumentation()
    {
        //TODO: fix
        //HACK: fix
        if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
        {
            return;
        }

        Assembly assembly = typeof(CarsController).Assembly;
        IServiceCollection services = new ServiceCollection();
        new Startup().ConfigureServices(services);

        var sourceCodeDir = @"C:\development\github\opensource\kasperbirkelund\StrongHeart\DemoApp\StrongHeart.DemoApp.WebApi";//CodeCommentSection.GetSourceCodeDirFromFeature<CarsController>(@"\DemoApp\");

        MarkDownVisitor visitor = new MarkDownVisitor();
        DocumentationGeneratorUtil.GenerateToVisitor(assembly, services, sourceCodeDir, visitor, x => x.DocName == DocumentationConstants.Setup);
        _helper.WriteLine(visitor.AsString());
    }
}