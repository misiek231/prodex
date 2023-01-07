using System.Text;

namespace Prodex.ClientGenerator
{
    public class ClientGenerator
    {
        public void Generate(EndpointDataSource endpoints)
        {
            var apiEndpoints = endpoints.Endpoints.Cast<RouteEndpoint>().Where(p => p.RoutePattern.RawText.Split("/").First() == "api");

            var groups = apiEndpoints.GroupBy(p => p.RoutePattern.RawText.Split("/")[1]);

            foreach (var group in groups)
            {
                var model = new ClientModel(group);
                var text = new Client
                {
                    Model = model,
                }.TransformText();

                using var f = File.OpenWrite($"../Prodex.Client/RestClients/{model.Name}Client.g.cs");

                f.Flush();
                f.Write(Encoding.UTF8.GetBytes(text));
            }
        }
    }
}
