using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Dictionary;

public class ApiDictionarySelect : IApiSelect
{
    public long Id { get; set; }
    public string Value { get; set; }

    public ApiDictionarySelect() { }

    public ApiDictionarySelect(long id)
    {
        Id = id;
    }

    public ApiDictionarySelect(long id, string value)
    {
        Id = id;
        Value = value;
    }
}

public class ApiDictionaryTermSelect : IApiSelect
{
    public long Id { get; set; }
    public string Value { get; set; }

    public ApiDictionaryTermSelect() { }

    public ApiDictionaryTermSelect(long id)
    {
        Id = id;
    }

    public ApiDictionaryTermSelect(long id, string value)
    {
        Id = id;
        Value = value;
    }
}