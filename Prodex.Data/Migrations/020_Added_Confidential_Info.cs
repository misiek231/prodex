using FluentMigrator;
using Prodex.Data.Extensions;

namespace Prodex.Data.Migrations
{
    [Migration(20)]
    public class AddedConfidentialInfo : Migration
    {
        public override void Up()
        {
            var tables = new string[]
            {
                "ProductTemplates",
                "Products",
                "FieldConfigs",
                "DynamicFieldValues",
                "PtStatuses",
                "Dictionaries",
                "DictionaryTerms",
                "Users",
                "ServiceTaskConfigs"
            };

            foreach (var p in tables)
            {
                Alter.Table(p).AddConfidential();
            }
        }

        public override void Down()
        {
        }
    }
}