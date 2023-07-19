using Prodex.Bussines.Requests;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Utils;
using Prodex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Bussines.Services
{
    public class ApiSelectCacheService
    {
        private Dictionary<string, Type> _cache; 

        public ApiSelectCacheService()
        {
            _cache = typeof(IApiSelect).Assembly.GetTypes()
               .Where(p => typeof(IApiSelect).IsAssignableFrom(p) && !p.IsInterface)
               .ToDictionary(p => p.Name.PascalToKebab(), p => typeof(GetSelectValuesRequest<>).MakeGenericType(p));
        }

        public GetSelectValuesRequest GetRequestByName(string name)
        {
            if (!_cache.TryGetValue(name, out var type))
                throw new ArgumentException("{} select not found in cache", name);

            return Activator.CreateInstance(type) as GetSelectValuesRequest;
        }
    }
}
