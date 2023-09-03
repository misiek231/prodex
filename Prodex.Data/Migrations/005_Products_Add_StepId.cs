using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(5)]
    public class PrductsAddStepId : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("CurrentStepId").AsInt64().NotNullable().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("CurrentStepId").FromTable("Products");
        }
    }
}