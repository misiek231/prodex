using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(1)]
    public class AddUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Username").AsString(60)
                .WithColumn("Password").AsString(60);
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
