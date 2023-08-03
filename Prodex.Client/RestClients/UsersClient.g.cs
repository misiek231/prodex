using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class UsersClient
    {
        private HttpClient client;

        public UsersClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Users.ListItemModel>> Get(Prodex.Shared.Pagination.Pager pager, Prodex.Shared.Models.Users.FilterModel model)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Pagination.Pagination<Prodex.Shared.Models.Users.ListItemModel>>("api/users/", TypeMerger.TypeMerger.Merge(pager, model));
        }

        public async System.Threading.Tasks.Task<Prodex.Shared.Models.Users.FormModel> Get(System.Int64 id)
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.Users.FormModel>($"api/users/{id}");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Post(Prodex.Shared.Models.Users.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PostAsJsonAsync("api/users/", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<System.Object>> Put(System.Int64 id, Prodex.Shared.Models.Users.FormModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<System.Object>(await client.PutAsJsonAsync($"api/users/{id}", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
