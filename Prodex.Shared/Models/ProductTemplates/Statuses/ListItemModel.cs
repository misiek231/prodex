using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.ProductTemplates.Statuses
{
    public class ListItemModel : ConfidentialInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
