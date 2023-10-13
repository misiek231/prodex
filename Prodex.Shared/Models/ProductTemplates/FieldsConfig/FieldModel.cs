using Prodex.Shared.Models.Dictionary;

namespace Prodex.Shared.Models.ProductTemplates.FieldsConfig;

public class FieldModel
{
    public long? Id { get; set; }
    public string DisplayName { get; set; }
    public FieldType Type { get; set; }
    public ApiDictionarySelect Dictionary {  get; set; }
}
