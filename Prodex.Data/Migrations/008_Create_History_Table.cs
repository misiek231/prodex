using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(8)]
    public class Create_History_Table : Migration
    {
        public override void Up()
        {
            Create.Table("History")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id")
                .WithColumn("ProductId").AsInt64().NotNullable().ForeignKey("Products", "Id")
                .WithColumn("ActionName").AsString();
        }

        public override void Down()
        {
            Delete.Table("History");
        }
    }
}