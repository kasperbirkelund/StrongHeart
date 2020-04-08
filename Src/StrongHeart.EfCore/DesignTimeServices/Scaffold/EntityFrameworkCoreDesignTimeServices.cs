using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable 1591
namespace StrongHeart.EfCore.DesignTimeServices.Scaffold
{
    /*This class MUST remain as it is used by Scaffold-DbContext power shell command*/
    public class EntityFrameworkCoreDesignTimeServices : IDesignTimeServices
    {
        //Used for scaffolding database to code
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpDbContextGenerator, StrongHeartDbContextGenerator>();
            serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, RemoveTemporalTableColumnsCSharpEntityTypeGenerator>();
            serviceCollection.AddSingleton<IReverseEngineerScaffolder, AddGeneratedClassToFileNameReverseEngineerScaffolder>();
        }
    }
}
#pragma warning restore 1591