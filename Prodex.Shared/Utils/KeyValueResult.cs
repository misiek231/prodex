namespace Prodex.Shared.Utils
{
    public class KeyValueResult
    {
        public long Key { get; set; }
        public string Value { get; set; }
        public KeyValueResult(long key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
