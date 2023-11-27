using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.DictionaryTerms;

public class ListItemModel : ConfidentialInfo
{
    public long Id { get; set; }
    public string Value { get; set; }
    public KeyValueResult Dictionary { get; set; }
}
