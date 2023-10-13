using System.ComponentModel;

namespace Prodex.Shared.Models.ProductTemplates.FieldsConfig;

public enum FieldType
{
    [Description("Pole tekstowe")]
    Text,
    
    [Description("Pole numeryczne")]
    Number,

    [Description("Pole słownikowe")]
    Dictionary,
}