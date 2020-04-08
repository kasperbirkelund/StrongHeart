using System;
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

        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, string connectionString)
        {
            var assembly = typeof(MigrationRunner).Assembly;
            services
                .AddFluentMigratorCore()
                .AddSingleton<IAssemblySourceItem>(x => new AssemblySourceItem(assembly))
                .ConfigureRunner(x => x
                    .AddSqlServer2016()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(assembly).For.Migrations());
            return services;
        }
    }
}