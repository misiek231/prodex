using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(5)]
    public class AddPrductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60)
                .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id");
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}