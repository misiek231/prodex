using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class FieldsClient
    {
        private HttpClient client;

        public FieldsClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldModel>> Get(System.Int64 templateId, Prodex.Shared.Pagination.Pager pager)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldModel>>($"api/fields/{templateId}", pager);
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldsConfigFormModel>> Post(System.Int64 templateId, Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldsConfigFormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldsConfigFormModel>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.ProductTemplates.FieldsConfig.FieldsConfigFormModel>(await client.PostAsJsonAsync($"api/fields/{templateId}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
