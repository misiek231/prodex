using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodex.Data.Models;

namespace Prodex.Data.Configurations;

public partial class FieldConfigConfiguration
{
    partial void OnConfigurePartial(EntityTypeBuilder<FieldConfig> entity)
    {
        entity.Ignore(p => p.FieldTypeEnum);
    }
}