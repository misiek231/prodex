using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(12)]
    public class Create_SendTaskConfig_Table : Migration
    {
        public override void Up()
        {
            Create.Table("SendTaskConfigs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id")
                .WithColumn("StepId").AsString(20).Unique().NotNullable()
                .WithColumn("TargetType").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id");
        }

        public override void Down()
        {
            Delete.Table("SendTaskConfigs");
        }
    }
}