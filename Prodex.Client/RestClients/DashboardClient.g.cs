using Prodex.Client.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace Prodex.Client.RestClients
{
    [Prodex.Client.DiHelpers.RegisterScoped]
    public class DashboardClient
    {
        private HttpClient client;

        public DashboardClient(HttpClient client)
        {
            this.client = client;
        }


        public async System.Threading.Tasks.Task<Prodex.Shared.Models.Dashboard.DashboardDetailsModel> Get()
        {
            return await client.GetFromJsonAsync<Prodex.Shared.Models.Dashboard.DashboardDetailsModel>("api/dashboard");
        }
    }
}
