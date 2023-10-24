using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(16)]
    public class History_Add_ActionId : Migration
    {
        public override void Up()
        {
            Alter.Table("History")
                .AddColumn("ActionId").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("ActionId").FromTable("History");
        }
    }
}