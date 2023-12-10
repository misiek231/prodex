using Prodex.Shared.Pagination;

namespace Prodex.Shared.Models.Dashboard;

public class WidgetModel
{
    public string Name { get; set; }
    public Pagination<Products.ListItemModel> Products { get; set; }
}
