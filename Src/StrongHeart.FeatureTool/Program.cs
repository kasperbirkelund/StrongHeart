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
        ////dotnet AddFeature  Commands OpretHenvendelse
        //string project = "DoktorGl.Features";//args[0];
        //string type = "Commands";//args[1];
        //string name = "OpretHenvendelse"; //args[2];
        //bool isList = false;
        //if (args.Length > 3)
        //{
        //    isList = Convert.ToBoolean(args[3]);
        //}
        //string dir = Path.Combine(Environment.CurrentDirectory, project);
        //dir = Path.Combine(dir, "Features");
        //dir = Path.Combine(dir, type);
        //dir = Path.Combine(dir, name);

        //Directory.CreateDirectory(dir);

        //string file = name + "Feature.cs";
        //string fullPath = Path.Combine(dir, file);

        //string content = type switch
        //{
        //    "Commands" => GetCommand(project, name),
        //    "Queries" => GetQuery(project, name, isList),
        //    _ => throw new ArgumentException("Unknown type. Use 'Commands' or 'Queries'")
        //};

        //File.WriteAllText(fullPath, content);
        //Console.WriteLine("Done: " + fullPath);
    }
}