using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Migrations;

namespace StrongHeart.DempApp.Migrations.Runner;

class Program
{
    static int Main(string[] args)
    {
        try
        {
            args = new[]
            {
                @"Server=.\sqlexpress;Database=Doktor;Trusted_Connection=True;"
            };
            using (IServiceScope scope = GetServiceScope(args))
            {
                scope.ServiceProvider.MigrateUp();
            }
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return -1;
    }

    private static IServiceScope GetServiceScope(string[] args)
    {
        IServiceCollection col = new ServiceCollection();
        col.AddFluentMigrator(typeof(Id0001_InitialSchema).Assembly, args[0]);
        IServiceProvider provider = col.BuildServiceProvider();
        return provider.CreateScope();
    }
}