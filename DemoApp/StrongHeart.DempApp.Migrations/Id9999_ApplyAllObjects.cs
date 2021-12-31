using FluentMigrator;
using StrongHeart.Migrations;

namespace StrongHeart.DempApp.Migrations;

[Maintenance(MigrationStage.AfterAll)]
public class Id9999_ApplyAllObjects : ForwardOnlyMigration
{
    public override void Up()
    {
        Execute.DropViewsAndFunctionsAndProcedures();
        Execute.ApplyAllSchemaObjects(GetType().Assembly);
    }
}