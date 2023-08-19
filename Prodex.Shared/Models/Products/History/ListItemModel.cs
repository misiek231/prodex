using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Products.History;

public class ListItemModel
{
    public KeyValueResult User { get; set; }
    public string ActionName { get; set; }
    public DateTime DateCreated { get; set; }
}
