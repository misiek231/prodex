using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(3)]
    public class UsersAddGivenNameAndSurname : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("GivenName").AsString(60).Nullable()
                .AddColumn("Surname").AsString(60).Nullable();
        }

        public override void Down()
        {
            Delete.Column("GivenName").Column("Surname").FromTable("Users");
        }
    }
}
