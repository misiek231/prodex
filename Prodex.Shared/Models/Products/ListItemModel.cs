using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Products;

public class ListItemModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public KeyValueResult Template { get; set; }
}
