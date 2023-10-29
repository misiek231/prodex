using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.ProductTemplates
{
    // TODO: add logic to handle this
    public class ApiFieldConfig : IApiSelect
    {
        public long Id { get; set; }
        public string Value { get; set; }

        public ApiFieldConfig() { }

        public ApiFieldConfig(long id)
        {
            Id = id;
        }

        public ApiFieldConfig(long id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
