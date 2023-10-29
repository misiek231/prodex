using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(17)]
    public class Create_SequenceFlowConfig_Table : Migration
    {
        public override void Up()
        {
            Create.Table("SequenceFlowConfigs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id")
                .WithColumn("FlowId").AsString(20).Unique().NotNullable()
                .WithColumn("LDynamicFieldId").AsInt64().Nullable().ForeignKey("FieldConfigs", "Id")
                .WithColumn("LValue").AsString(1024).Nullable()
                .WithColumn("OperatorType").AsInt32().NotNullable()
                .WithColumn("RDynamicFieldId").AsInt64().Nullable().ForeignKey("FieldConfigs", "Id")
                .WithColumn("RValue").AsString(1024).Nullable();
        }

        public override void Down()
        {
            Delete.Table("SequenceFlowConfigs");
        }
    }
}