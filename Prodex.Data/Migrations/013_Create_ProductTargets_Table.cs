using FluentMigrator;

namespace Prodex.Data.Migrations;

[Migration(13)]
public class Create_ProductTargets_Table : Migration
{
    public override void Up()
    {
        Create.Table("ProductTargets")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("ProductId").AsInt64().NotNullable().ForeignKey("Products", "Id")
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id");
    }

    public override void Down()
    {
        Delete.Table("ProductTargets");
    }
}