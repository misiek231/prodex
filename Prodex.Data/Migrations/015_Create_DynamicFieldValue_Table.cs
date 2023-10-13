using FluentMigrator;

namespace Prodex.Data.Migrations;

[Migration(15)]
public class Create_DynamicFieldValue_Table : Migration
{
    public override void Up()
    {
        Create.Table("DynamicFieldValues")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("FieldConfigId").AsInt64().NotNullable().ForeignKey("FieldConfigs", "Id")
            .WithColumn("ProductId").AsInt64().NotNullable().ForeignKey("Products", "Id")
            .WithColumn("Value").AsString(5000).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("DynamicFieldValues");
    }
}