using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Execute;

namespace StrongHeart.Migrations
{
    public static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithExternalIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("ExternalId").AsGuid().NotNullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithCreatedAtUtc(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("CreatedAtUtc").AsCustom("DATETIME2(0)").NotNullable();
        }

        public static void ApplyAllSchemaObjects(this IExecuteExpressionRoot root, Assembly assembly, Func<string, string> scriptSortAlgorithm)
        {
            static string GetFileName(string arg) => arg.Split().Last();

            IEnumerable<string> scripts = assembly.GetManifestResourceNames().Where(x => x.Contains(".Schema.")).Select(GetFileName).OrderBy(scriptSortAlgorithm);
            foreach (string script in scripts)
            {
                root.EmbeddedScript(script);
            }
        }

        public static void ApplyAllSchemaObjects(this IExecuteExpressionRoot root, Assembly assembly)
        {
            root.ApplyAllSchemaObjects(assembly, x => x);
        }

        public static void DropViewsAndFunctionsAndProcedures(this IExecuteExpressionRoot root)
        {
            const string dropAllProc = @"DECLARE @sql VARCHAR(MAX)='';

SELECT @sql=@sql+'drop procedure [' + name + '];' FROM sys.objects 
WHERE type = 'p' AND  is_ms_shipped = 0

exec(@sql);";

            const string dropAllFunc = @"DECLARE @sql VARCHAR(MAX)='';

SELECT @sql=@sql+'drop function [' + name + '];' FROM sys.objects 
WHERE type = 'FN' AND  is_ms_shipped = 0

exec(@sql);";

            const string dropAllViews = @"DECLARE @sql VARCHAR(MAX)='';

SELECT @sql=@sql+'drop view [' + name + '];' FROM sys.objects 
WHERE type = 'v' AND  is_ms_shipped = 0

exec(@sql);";

            root.Sql(dropAllFunc);
            root.Sql(dropAllProc);
            root.Sql(dropAllViews);
        }
    }
}
