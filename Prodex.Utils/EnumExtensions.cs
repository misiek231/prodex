using System.ComponentModel;

namespace Prodex.Utils;

public static class EnumExtensions
{
    public static IDictionary<TEnum, string> GetValuesWithDescription<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(p => p, p => p.Description());
    }

    public static string Description(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : "Description Not Found";
    }
}
