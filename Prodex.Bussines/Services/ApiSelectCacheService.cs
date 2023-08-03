using Prodex.Bussines.Handlers.Selects;
using Prodex.Shared.Utils;
using Prodex.Utils;

namespace Prodex.Bussines.Services
{
    public class ApiSelectCacheService
    {
        private Dictionary<string, Type> _cache;

        public ApiSelectCacheService()
        {
            _cache = typeof(IApiSelect).Assembly.GetTypes()
               .Where(p => typeof(IApiSelect).IsAssignableFrom(p) && !p.IsInterface)
               .ToDictionary(p => p.Name.PascalToKebab(), p => typeof(SelectRequest<>).MakeGenericType(p));
        }

        public SelectRequest GetRequestByName(string name)
        {
            if (!_cache.TryGetValue(name, out var type))
                throw new ArgumentException("{} select not found in cache", name);

            return Activator.CreateInstance(type) as SelectRequest;
        }
    }
}
