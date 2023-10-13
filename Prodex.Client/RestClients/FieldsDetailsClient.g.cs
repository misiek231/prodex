using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class FieldsDetailsClient
    {
        private HttpClient client;

        public FieldsDetailsClient(HttpClient client)
        {
            this.client = client;
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<Prodex.Shared.Models.Products.DynamicFields.DynamicFieldDetailsModel>> Get(System.Int64 productId)
        {
            return await client.GetFromJsonAsync<System.Collections.Generic.List<Prodex.Shared.Models.Products.DynamicFields.DynamicFieldDetailsModel>>($"api/fields-details/{productId}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Post(System.Int64 productId, System.Int64 fieldConfigId, Prodex.Shared.Models.Products.DynamicFields.SetProductFieldFormModel model)
        {
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync($"api/fields-details/{productId}/{fieldConfigId}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
