using FluentMigrator;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Data.Extensions;

public static class MigrationExtensions
{
    public static void AddConfidential(this IAlterTableAddColumnOrAlterColumnSyntax builder)
    {
        builder
            .AddColumn("CreatedBy").AsInt64().Nullable().ForeignKey("Users", "Id")
            .AddColumn("UpdatedBy").AsInt64().Nullable().ForeignKey("Users", "Id")
            .AddColumn("DateCreatedUtc").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .AddColumn("DateUpdatedUtc").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }
}
