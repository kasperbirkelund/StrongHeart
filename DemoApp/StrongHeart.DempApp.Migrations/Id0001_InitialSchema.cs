using FluentMigrator;
using StrongHeart.Migrations;

namespace StrongHeart.DempApp.Migrations;

[Migration(20211230103300)]
public class Id0001_InitialSchema : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("DemoTable").InSchema("dbo")
            .WithIdColumn()
            .WithExternalIdColumn()
            .WithCreatedAtUtc();
    }
}