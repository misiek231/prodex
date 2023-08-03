using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class DictionaryTermsClient
    {
        private HttpClient client;

        public DictionaryTermsClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.DictionaryTerms.ListItemModel>> Get(Prodex.Shared.Pagination.Pager pager, Prodex.Shared.Models.DictionaryTerms.FilterModel model)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.DictionaryTerms.ListItemModel>>("api/dictionary-terms/", TypeMerger.TypeMerger.Merge(pager, model));
        }

        public async System.Threading.Tasks.Task<Prodex.Shared.Models.DictionaryTerms.FormModel> Get(System.Int64 id)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.DictionaryTerms.FormModel>($"api/dictionary-terms/{id}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Post(Prodex.Shared.Models.DictionaryTerms.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync("api/dictionary-terms/", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Put(System.Int64 id, Prodex.Shared.Models.DictionaryTerms.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/dictionary-terms/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
