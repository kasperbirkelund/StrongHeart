using System;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Migrations
{
    public static class MigrationRunner
    {
        public static void MigrateUp(this IServiceProvider serviceProvider)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, Assembly migrationAssembly, string connectionString)
        {
            services
                .AddFluentMigratorCore()
                .AddSingleton<IAssemblySourceItem>(x => new AssemblySourceItem(migrationAssembly))
                .AddLogging(x=> x.AddFluentMigratorConsole())
                .ConfigureRunner(x => x
                    .AddSqlServer2016()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(migrationAssembly).For.Migrations());
            return services;
        }
    }
}