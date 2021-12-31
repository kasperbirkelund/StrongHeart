using FluentMigrator;

namespace StrongHeart.DempApp.Migrations;

[Migration(20211231153100)]
public class Id0002_AddData : ForwardOnlyMigration
{
    public override void Up()
    {
        Insert.IntoTable("DemoTable").InSchema("dbo")
            .Row(new
            {
                ExternalId = Guid.NewGuid(),
                CreatedAtUtc = DateTime.UtcNow
            })
            .Row(new
            {
                ExternalId = Guid.NewGuid(),
                CreatedAtUtc = DateTime.UtcNow
            })
            .Row(new
            {
                ExternalId = Guid.NewGuid(),
                CreatedAtUtc = DateTime.UtcNow
            });
    }
}