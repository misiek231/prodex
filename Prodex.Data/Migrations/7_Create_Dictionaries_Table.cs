using FluentMigrator;

namespace Prodex.Data.Migrations
{
    [Migration(7)]
    public class Create_Dictionaries_Table : Migration
    {
        public override void Up()
        {
            Create.Table("Dictionaries")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60);

            Create.Table("DictionaryTerms")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsString(60)
                .WithColumn("DictionaryId").AsInt64().NotNullable().ForeignKey("Dictionaries", "Id");
        }

        public override void Down()
        {
            Delete.Table("Dictionaries");
            Delete.Table("DictionaryTerms");
        }
    }
}