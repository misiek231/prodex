using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(4)]
    public class AddPrductTemplatesTable : Migration
    {
        public override void Up()
        {
            Create.Table("ProductTemplates")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60)
                .WithColumn("ProcessXml").AsString(8000).Nullable();
        }

        public override void Down()
        {
            Delete.Table("ProductTemplates");
        }
    }
}
