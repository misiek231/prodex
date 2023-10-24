using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Products;

public class TemplateDetailsModel : KeyValueResult
{
    public string Xml { get; set; }

    public TemplateDetailsModel(long key, string value, string xml) : base(key, value)
    {
        Xml = xml;
    }
}