using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(4)]
    public class AddPrductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60)
                .WithColumn("ProcessId").AsInt64().ForeignKey("Processes", "Id").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
