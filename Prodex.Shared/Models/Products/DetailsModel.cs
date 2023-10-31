using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Products;

public class DetailsModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsFinished { get; set; }
    public TemplateDetailsModel Template { get; set; }
    public ApiStatus Status { get; set; }
    public List<ApiButton> Buttons { get; set; }
}
