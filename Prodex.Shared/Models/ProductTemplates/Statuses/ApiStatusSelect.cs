using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.ProductTemplates
{
    public class ApiStatusSelect : IApiSelect
    {
        public long Id { get; set; }
        public string Value { get; set; }

        public ApiStatusSelect() { }

        public ApiStatusSelect(long id)
        {
            Id = id;
        }

        public ApiStatusSelect(long id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
