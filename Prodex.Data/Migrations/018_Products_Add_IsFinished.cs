using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(18)]
    public class PrductsAddIsFinoshed : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("IsFinished").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("IsFinished").FromTable("Products");
        }
    }
}