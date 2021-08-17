using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StrongHeart.Core.FeatureToggling;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.DemoApp.Business.Features.Queries.GetCar;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.DemoApp.WebApi.Toggles;
using StrongHeart.Features.Core.Events;
using StrongHeart.Features.DependencyInjection;

namespace StrongHeart.DemoApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFoo, Foo>();
            services.AddTransient<IConfigurationReader, ConfigurationReaderImpl>();
            services.AddTransient<IClaimsProvider, MyCustomClaimsProvider>();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSingleton<IFeatureToggle<MyToggle>, MyToggle>();
            services.AddSingleton<IEventPublisher, SimpleEventPublisher>();

            services.AddStrongHeart(options =>
            {
                options.AddDefaultPipeline<MyCustomExceptionLogger, MyCustomTimeAlertExceededLogger>();
            }, typeof(CommandFeatureBase<,>).Assembly);

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
    }
}