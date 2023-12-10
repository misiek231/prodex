namespace Prodex.Shared.Models.Dashboard;

public class DashboardDetailsModel
{
    public List<WidgetModel> Widgets { get; set; }

    public bool IsAdmin { get; set; }
}
