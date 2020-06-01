using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator;
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
                .WithColumn("CreatedAtUtc").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        public static void ApplyAllSchemaObjects(this IExecuteExpressionRoot root, Assembly assembly, Func<string, string> scriptSortAlgorithm)
        {
            string GetFileName(string arg)
            {
                return arg.Split().Last();
            }

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

SELECT @sql=@sql+'drop procedure ['+name +'];' FROM sys.objects 
WHERE type = 'p' AND  is_ms_shipped = 0

exec(@sql);";

            const string dropAllFunc = @"DECLARE @sql VARCHAR(MAX)='';

SELECT @sql=@sql+'drop function ['+name +'];' FROM sys.objects 
WHERE type = 'FN' AND  is_ms_shipped = 0

exec(@sql);";

            const string dropAllViews = @"DECLARE @sql VARCHAR(MAX)='';

SELECT @sql=@sql+'drop view ['+name +'];' FROM sys.objects 
WHERE type = 'v' AND  is_ms_shipped = 0

exec(@sql);";

            root.Sql(dropAllFunc);
            root.Sql(dropAllProc);
            root.Sql(dropAllViews);
        }

        //Creates sql server controlled history. Documentation: https://docs.microsoft.com/en-us/sql/relational-databases/tables/temporal-tables?view=sql-server-ver15
        public static void CreateTemporalTable(this IExecuteExpressionRoot root, string schema, string table)
        {
            root.Sql(@$"ALTER TABLE {schema}.{table} ADD
                RowValidFromUtc DATETIME2(0) GENERATED ALWAYS AS ROW START, 
                RowValidToUtc DATETIME2(0) GENERATED ALWAYS AS ROW END,
                PERIOD FOR SYSTEM_TIME RowValidFromUtc, RowValidToUtc);");

            root.Sql($"ALTER TABLE {schema}.{table} SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = {schema}.{table}History))");
        }
    }
}
