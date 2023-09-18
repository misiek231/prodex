using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class ProductTemplatesConfigsClient
    {
        private HttpClient client;

        public ProductTemplatesConfigsClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Models.ProductTemplates.ElementOptions.ServiceTaskConfigFormModel> GetServiceTaskConfig(System.Int64 templateId, System.String stepId)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.ProductTemplates.ElementOptions.ServiceTaskConfigFormModel>($"api/product-templates-configs/service-task/{templateId}/{stepId}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> CreateServiceTaskConfig(System.Int64 templateId, System.String stepId, Prodex.Shared.Models.ProductTemplates.ElementOptions.ServiceTaskConfigFormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync($"api/product-templates-configs/service-task/{templateId}/{stepId}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> UpdateServiceTaskConfig(System.Int64 id, Prodex.Shared.Models.ProductTemplates.ElementOptions.ServiceTaskConfigFormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/product-templates-configs/service-task/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Shared.Models.ProductTemplates.ElementOptions.SendTaskConfigFormModel> GetSendTaskConfig(System.Int64 templateId, System.String stepId)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.ProductTemplates.ElementOptions.SendTaskConfigFormModel>($"api/product-templates-configs/send-task/{templateId}/{stepId}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> CreateSendTaskConfig(System.Int64 templateId, System.String stepId, Prodex.Shared.Models.ProductTemplates.ElementOptions.SendTaskConfigFormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync($"api/product-templates-configs/send-task/{templateId}/{stepId}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> UpdateSendTaskConfig(System.Int64 id, Prodex.Shared.Models.ProductTemplates.ElementOptions.SendTaskConfigFormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/product-templates-configs/send-task/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
