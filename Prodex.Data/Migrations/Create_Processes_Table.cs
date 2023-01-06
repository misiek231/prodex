using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(1)]
    public class AddProcessesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Processes")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60)
                .WithColumn("Xml").AsString();
        }

        public override void Down()
        {
            Delete.Table("Processes");
        }
    }
}
