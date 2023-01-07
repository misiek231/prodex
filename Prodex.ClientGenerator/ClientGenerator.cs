using Prodex.Shared.Models.Processes;
using System.Reflection;
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
                var endpointsModels = new List<EndpointModel>();

                foreach (var item in group)
                {
                    var parameters = new List<Parameter>();

                    foreach (var param in item.Metadata.GetRequiredMetadata<MethodInfo>().GetParameters())
                    {
                        if (param.ParameterType.Assembly == typeof(FilterModel).Assembly)
                            parameters.Add(new Parameter
                            {
                                Name = param.Name,
                                Type = param.ParameterType.FullName
                            });
                    }

                    var methodInfo = item.Metadata.GetRequiredMetadata<MethodInfo>();

                    var name = item.RoutePattern.RawText.Split("/").ElementAtOrDefault(3)?.FirstLetterUpper();

                    var method = item.Metadata.GetRequiredMetadata<HttpMethodMetadata>().HttpMethods.First();

                    endpointsModels.Add(new EndpointModel
                    {
                        Method = method,
                        MethodName = name ?? method.FirstLetterUpper(),
                        Route = item.RoutePattern.RawText,
                        ReturnType = methodInfo.ReturnParameter.ToString().FixTypeDefinition(),
                        ResponseType = methodInfo.ReturnParameter.ToString().FixTypeDefinition().RemoveTask(),
                        Parameters = parameters
                    });
                }

                var model = new ClientModel
                {
                    Name = group.Key.FirstLetterUpper(),
                    Endpoints = endpointsModels
                };

                var template = new Client
                {
                    Model = model,
                };

                var text = template.TransformText();

                using var f = File.OpenWrite($"../Prodex.Client/RestClients/{model.Name}Client.g.cs");

                f.Flush();
                f.Write(Encoding.UTF8.GetBytes(text));
            }
        }
    }
}
