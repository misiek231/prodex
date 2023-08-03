using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class ProductsClient
    {
        private HttpClient client;

        public ProductsClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Products.ListItemModel>> Get(Prodex.Shared.Pagination.Pager pager, Prodex.Shared.Models.Products.FilterModel model)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Products.ListItemModel>>("api/products/", TypeMerger.TypeMerger.Merge(pager, model));
        }

        public async System.Threading.Tasks.Task<Prodex.Shared.Models.Products.DetailsModel> Get(System.Int64 id)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.Products.DetailsModel>($"api/products/{id}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Post(Prodex.Shared.Models.Products.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync("api/products/", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Put(System.Int64 id, Prodex.Shared.Models.Products.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/products/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Execute(System.Int64 productId, System.Int64 stepId)
        {
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsync($"api/products/execute/{productId}/{stepId}", null));
            await result.InitResultAsync();
            
            return result;
        }
    }
}
