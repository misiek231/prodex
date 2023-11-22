using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(19)]
    public class UsersAddUserType : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Email").AsString(60).Nullable()
                .AddColumn("UserType").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete
                .Column("Email")
                .Column("UserType")
                .FromTable("Users");
        }
    }
}