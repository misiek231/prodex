using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(11)]
    public class PrductsAddStatus : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("StatusId").AsInt64().Nullable().ForeignKey("PtStatuses", "Id");
        }

        public override void Down()
        {
            Delete.Column("StatusId").FromTable("Products");
        }
    }
}