namespace Prodex.ClientGenerator
{
    public static class Extensions
    {
        public static string FirstLetterUpper(this string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1).ToLower();
        }

        public static string FixTypeDefinition(this string value)
        {
            return value.Replace("`1[", "<").Replace("]", ">");
        }

        public static string RemoveTask(this string value)
        {
            var removed = value.Replace("System.Threading.Tasks.Task<", "");
            return removed.Remove(removed.Length - 1);
        }
    }
}
