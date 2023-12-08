using Prodex.Data.Interfaces;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;

namespace Prodex.Data.Models;

public partial class FieldConfig : IEntity, IConfidential
{
    public FieldType FieldTypeEnum { get => (FieldType)Type; set => Type = (int)value; }
}
