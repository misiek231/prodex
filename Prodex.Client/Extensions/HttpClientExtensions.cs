using System.Net.Http.Json;

namespace Prodex.Client.Extensions
{
    public static class HttpClientExtensions
    {

        public static async Task<T> GetFromJsonAsync<T>(this HttpClient client, string path, object param)
        {
            return await client.GetFromJsonAsync<T>(path + ToQueryString(param));
        }


        private static string ToQueryString(object entity)
        {
            var items = entity.GetType().GetProperties()
                .Select(p => $"{p.Name}={p.GetValue(entity)}")
                .ToList();

            if (items.Count == 0)
                return "";

            return $"?{string.Join("&", items)}";
        }
    }
}
