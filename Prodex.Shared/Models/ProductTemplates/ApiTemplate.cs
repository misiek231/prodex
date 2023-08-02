using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.ProductTemplates
{
    public class ApiTemplateSelect : IApiSelect
    {
        public long Id { get; set; }
        public string Value { get; set; }

        public ApiTemplateSelect() { }

        public ApiTemplateSelect(long id)
        {
            Id = id;
        }

        public ApiTemplateSelect(long id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
