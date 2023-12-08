using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(21)]
    public class Update_SequenceFlowConfig_Table : Migration
    {
        public override void Up()
        {
            Alter.Table("SequenceFlowConfigs")
                .AddColumn("LDictionaryTermId").AsInt64().Nullable().ForeignKey("DictionaryTerms", "Id")
                .AddColumn("RDictionaryTermId").AsInt64().Nullable().ForeignKey("DictionaryTerms", "Id");
        }

        public override void Down()
        {
            Delete.Column("LDictionaryTermId").Column("RDictionaryTermId").FromTable("SequenceFlowConfigs");
        }
    }
}