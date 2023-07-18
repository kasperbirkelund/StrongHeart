using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

class Program
{
    static int Main(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            {
                config
                    .AddCommand<AddCommandFeature>("command")
                    .WithDescription("Create a new Command feature");

                config
                    .AddCommand<AddQuerySingleFeature>("querySingle")
                    .WithDescription("Create a new Query feature which returns a single result");

                config
                    .AddCommand<AddQueryListFeature>("queryList")
                    .WithDescription("Create a new Query feature which returns a list of result objects");

                config.AddExample(
                    "dotnet AddFeature command --feature-name=CreateCar --project-name=\"DoktorGl.Features\" --generate-partial-file --subfolder-name=Patient",
                    "dotnet AddFeature querySingle --feature-name=GetCar --project-name=\"DoktorGl.Features\" --generate-partial-file --subfolder-name=Patient",
                    "dotnet AddFeature querylist --feature-name=GetCars --project-name=\"DoktorGl.Features\" --generate-partial-file --subfolder-name=Patient");
            }
        });

        return app.Run(args);
    }
}