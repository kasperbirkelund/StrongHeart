#pragma warning disable 1591
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Xxx
{
    /*This class MUST remain as it is used by Scaffold-DbContext power shell command*/
    public class EntityFrameworkCoreDesignTimeServices : IDesignTimeServices
    {
        //Used for scaffolding database to code
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            ApplicationSpecificOptions options = new ApplicationSpecificOptions(new string[0]);
            serviceCollection.AddSingleton(options);
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, StrongHeartDbContextGenerator>();
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, StrongHeartCSharpEntityTypeGenerator>();
            serviceCollection.AddSingleton<IReverseEngineerScaffolder, StrongHeartReverseEngineerScaffolder>();
        }
    }
}
#pragma warning restore 1591