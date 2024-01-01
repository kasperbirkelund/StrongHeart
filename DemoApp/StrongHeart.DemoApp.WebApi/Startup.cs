using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StrongHeart.Core.FeatureToggling;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.Business.Events;
using StrongHeart.DemoApp.Business.Features.Commands;
using StrongHeart.DemoApp.Business.Features.EventHandlers;
using StrongHeart.DemoApp.Business.Features.EventHandlers.CarCreated;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.DemoApp.WebApi.Controllers;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.DemoApp.WebApi.Toggles;
using StrongHeart.Features.Core.Events;
using StrongHeart.Features.DependencyInjection;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.WebApi;

public class Startup : IDocumentationDescriber
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IFoo, Foo>();
        services.AddTransient<IConfigurationReader, ConfigurationReaderImpl>();
        services.AddScoped<IClaimsProvider, MyCustomClaimsProvider>();
        services.AddScoped<ICallerProvider, WebApiCallerProvider>();
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddSingleton<IFeatureToggle<MyToggle>, MyToggle>();
        services.AddSingleton<IEventPublisher, SimpleEventPublisher>();
        services.AddTransient<IEventHandler<CarCreatedEvent, DemoAppSpecificMetadata>, CarCreatedHandler>();

        //DOC-START Add StrongHeart to your IServiceCollection. Here the default pipeline is used.
        services.AddStrongHeart(options =>
        {
            options.AddDefaultPipeline<MyCustomExceptionLogger, MyCustomTimeAlertExceededLogger>();
        }, typeof(CommandFeatureBase<,>).Assembly);
        //DOC-END

        //Swagger is good for testing the api. Not important for StrongHeart
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "StrongHeart.DemoApp.WebApi", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StrongHeart.DemoApp.WebApi v1"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public string? DocName => DocumentationConstants.Setup;
    public int? Order => 1;

    public IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context)
    {
        yield return new TextSection("Changes required in the top level assembly (eg. a WebApi)", true);
        yield return new CodeCommentSection(GetType());
    }
}