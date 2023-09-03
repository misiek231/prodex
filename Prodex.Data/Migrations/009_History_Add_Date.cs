using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(9)]
    public class History_Add_Date : Migration
    {
        public override void Up()
        {
            Alter.Table("History")
                .AddColumn("DateCreated").AsDateTime2().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("DateCreated").FromTable("History");
        }
    }
}