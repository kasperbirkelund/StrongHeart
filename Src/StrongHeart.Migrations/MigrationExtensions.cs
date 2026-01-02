using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Execute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StrongHeart.Migrations;

public static class MigrationExtensions
{
    extension(ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        public ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn()
    {
        return tableWithColumnSyntax
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();
    }

    public ICreateTableColumnOptionOrWithColumnSyntax WithExternalIdColumn()
    {
        return tableWithColumnSyntax
            .WithColumn("ExternalId").AsGuid().NotNullable();
    }

    public ICreateTableColumnOptionOrWithColumnSyntax WithCreatedAtUtc()
    {
        return tableWithColumnSyntax
            .WithColumn("CreatedAtUtc").AsCustom("DATETIME2(0)").NotNullable();
    }
    }

    extension(IExecuteExpressionRoot root)
    {
        public void ApplyAllSchemaObjects(Assembly assembly, Func<string, string> scriptSortAlgorithm)
    {
        static string GetFileName(string arg) => arg.Split().Last();

        IEnumerable<string> scripts = assembly.GetManifestResourceNames().Where(x => x.Contains(".Schema.")).Select(GetFileName).OrderBy(scriptSortAlgorithm);
        foreach (string script in scripts)
        {
            root.EmbeddedScript(script);
        }
    }

    public void ApplyAllSchemaObjects(Assembly assembly)
    {
        root.ApplyAllSchemaObjects(assembly, x => x);
    }

    public void DropViewsAndFunctionsAndProcedures()
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