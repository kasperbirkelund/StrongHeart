#pragma warning disable 1591
#pragma warning disable EF1001

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.EfCore.DesignTimeServices.Scaffold.CustomGenerators;

namespace StrongHeart.DempApp.EfCoreConsole
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string connectionString = @"Server=.\sqlexpress;Database=xxx;Trusted_Connection=True;";
            IServiceCollection services = new ServiceCollection();
        }
    }

    /*This class MUST remain as it is used by Scaffold-DbContext power shell command*/
    public class EntityFrameworkCoreDesignTimeServices : IDesignTimeServices
    {
        //Used for scaffolding database to code
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            ApplicationSpecificOptions options = new ApplicationSpecificOptions(Array.Empty<string>());
            serviceCollection.AddSingleton(options);
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, StrongHeartDbContextGenerator>();
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, StrongHeartCSharpEntityTypeGenerator>();
            serviceCollection.AddSingleton<IReverseEngineerScaffolder, StrongHeartReverseEngineerScaffolder>();
        }
    }
}
#pragma warning restore 1591
#pragma warning restore EF1001