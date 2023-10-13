using FluentMigrator;

namespace Prodex.Data.Migrations;

[Migration(14)]
public class Create_FieldConfig_Table : Migration
{
    public override void Up()
    {
        Create.Table("FieldConfigs")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("TemplateId").AsInt64().NotNullable().ForeignKey("ProductTemplates", "Id")
            .WithColumn("DisplayName").AsString(50).NotNullable()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("DictionaryId").AsInt64().Nullable().ForeignKey("Dictionaries", "Id");
    }

    public override void Down()
    {
        Delete.Table("FieldConfigs");
    }
}