using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(7)]
    public class Create_Pt_Statuses_Table : Migration
    {
        public override void Up()
        {
            Create.Table("PtStatuses")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60)
                .WithColumn("Color").AsString(60)
                .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id");
        }

        public override void Down()
        {
            Delete.Table("PtStatuses");
        }
    }
}