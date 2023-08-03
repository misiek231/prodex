using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class DictionaryClient
    {
        private HttpClient client;

        public DictionaryClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Dictionary.ListItemModel>> Get(Prodex.Shared.Pagination.Pager pager, Prodex.Shared.Models.Dictionary.FilterModel model)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Dictionary.ListItemModel>>("api/dictionary/", TypeMerger.TypeMerger.Merge(pager, model));
        }

        public async System.Threading.Tasks.Task<Prodex.Shared.Models.Dictionary.FormModel> Get(System.Int64 id)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.Dictionary.FormModel>($"api/dictionary/{id}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Post(Prodex.Shared.Models.Dictionary.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync("api/dictionary/", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Put(System.Int64 id, Prodex.Shared.Models.Dictionary.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/dictionary/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
