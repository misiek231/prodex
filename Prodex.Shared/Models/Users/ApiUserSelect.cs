using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Users;

public class ApiUserSelect : IApiSelect
{
    public long Id { get; set; }
    public string Value { get; set; }

    public ApiUserSelect() { }

    public ApiUserSelect(long id)
    {
        Id = id;
    }

    public ApiUserSelect(long id, string value)
    {
        Id = id;
        Value = value;
    }
}
