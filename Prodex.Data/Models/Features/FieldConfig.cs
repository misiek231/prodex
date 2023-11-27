using Microsoft.VisualBasic.FileIO;
using Prodex.Data.Interfaces;

namespace Prodex.Data.Models;

public partial class FieldConfig : IEntity, IConfidential
{
    public FieldType FieldTypeEnum { get => (FieldType)Type; set => Type = (int)value; }
}
