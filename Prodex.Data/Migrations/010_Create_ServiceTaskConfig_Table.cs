using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(10)]
    public class Create_ServiceTaskConfig_Table : Migration
    {
        public override void Up()
        {
            Create.Table("ServiceTaskConfigs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id")
                .WithColumn("StepId").AsString(20).Unique().NotNullable()
                .WithColumn("ActionType").AsInt32().NotNullable()
                .WithColumn("ConfigJson").AsString(2048).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ServiceTaskConfigs");
        }
    }
}