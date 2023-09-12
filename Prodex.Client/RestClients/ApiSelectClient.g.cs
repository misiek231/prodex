using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class ApiSelectClient
    {
        private HttpClient client;

        public ApiSelectClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<System.Collections.Generic.List<Prodex.Shared.Utils.KeyValueResult>> Get(System.String name, Prodex.Shared.Pagination.Pager pager, System.String search, object additionalFilter)
        {
            return await client.GetFromJsonAsync<System.Collections.Generic.List<Prodex.Shared.Utils.KeyValueResult>>($"api/api-select/{name}", TypeMerger.TypeMerger.Merge(additionalFilter, TypeMerger.TypeMerger.Merge(pager, search)));
        }
    }
}
