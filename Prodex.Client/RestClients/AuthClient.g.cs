using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class AuthClient
    {
        private HttpClient client;

        public AuthClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Models.Sitemap.SitemapModel> Sitemap()
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.Sitemap.SitemapModel>("api/auth/sitemap");
        }

        public async System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.Auth.TokenModel>> Login(Prodex.Shared.Models.Auth.LoginModel model)
        {
            if (model.Validate(null).HasErrors)
                return new Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.Auth.TokenModel>(System.Net.HttpStatusCode.BadRequest);
            
            var result = new Prodex.Client.RestClients.HttpResponseMessage<Prodex.Shared.Models.Auth.TokenModel>(await client.PostAsJsonAsync("api/auth/login", model));
            await result.InitResultAsync();
            
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                model.WithErrors(await result.Content.ReadAsStringAsync());
            }
            
            return result;
        }
    }
}
