namespace Prodex.Utils
{
    public static class CasingExtensions
    {
        public static string PascalToKebab(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
