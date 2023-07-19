using CsCodeGenerator.Enums;
using CsCodeGenerator;
using Microsoft.AspNetCore.Routing;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Prodex.ClientGenerator
{
    public class ClientGenerator
    {
        public void Generate(EndpointDataSource endpoints)
        {
            var apiEndpoints = endpoints.Endpoints.Cast<RouteEndpoint>().Where(p => p.RoutePattern.RawText.Split("/").First() == "api");

            var groups = apiEndpoints.GroupBy(p => p.RoutePattern.RawText.Split("/")[1]);

            /*foreach (var group in groups)
            {
                var model = new ClientModel(group);
                var text = new Client
                {
                    Model = model,
                }.TransformText();

                File.WriteAllText($"../../../../Prodex.Client/RestClients/{model.Name}Client.g.cs", text);
            }
*/
            var files = groups.Select(g => GenerateClient(new ClientModel(g)));

            var csGenerator = new CsGenerator();
            csGenerator.OutputDirectory = "../../../../Prodex.Client/RestClients";
            csGenerator.Files.AddRange(files);
            csGenerator.CreateFiles(); 
        }

        public FileModel GenerateClient(ClientModel client)
        {
            var clientFile = new FileModel($"{client.Name}Client.g");

            clientFile.LoadUsingDirectives(new List<string>
            {
                "Prodex.Client.Extensions;",
                "System.Net;",
                "System.Net.Http.Json;"
            });
            
            clientFile.Namespace = "Prodex.Client.RestClients";

            var clientClass = new ClassModel(client.Name + "Client");

            clientClass.Attributes.Add(new AttributeModel("Prodex.Client.DiHelpers.RegisterScoped"));
            
            var conts = new Constructor(clientClass.Name);
            conts.Parameters.Add(new CsCodeGenerator.Parameter("HttpClient", "client"));
            conts.BodyLines.Add("this.client = client;");
            clientClass.Constructors.Add(conts);

            clientClass.Fields = new List<Field>
            {
                new Field("HttpClient", "client") { AccessModifier = AccessModifier.Private }
            };

            clientClass.Methods = client.Endpoints.Select(GenerateMethod).ToList();
                        
            clientFile.Classes.Add(clientClass);

            return clientFile;
        }

        private Method GenerateMethod(EndpointModel model)
        {
            return new Method(model.ReturnType, model.MethodName)
            {
                AccessModifier = AccessModifier.Public,
                SingleKeyWord = KeyWord.Async,
                Parameters = model.Parameters.Select(p => new CsCodeGenerator.Parameter(p.Type, p.Name)).ToList(),
                BodyLines = GenerateBody(model)
            };
        }
        
        private List<string> GenerateBody(EndpointModel model)
        {
            var body = new List<string>();

            if(model.Method == "POST")
            {
                if (model.Validate)
                {
                    body.Add("if (model.Validate(null).HasErrors)");
                    body.Add($"    return new Prodex.Client.RestClients.HttpResponseMessage<{model.ResponseType}>(System.Net.HttpStatusCode.BadRequest);");
                    body.Add("");
                }

                body.Add($"var result = new Prodex.Client.RestClients.HttpResponseMessage<{model.ResponseType}>(await client.PostAsJsonAsync(\"{model.Route}\", model));");
                body.Add("await result.InitResultAsync();");
                body.Add("");
                body.Add("if (result.StatusCode == HttpStatusCode.BadRequest)");
                body.Add("{");
                body.Add("    model.WithErrors(await result.Content.ReadAsStringAsync());");
                body.Add("}");
                body.Add("");
                body.Add("return result;");
            } 
            else if(model.Method == "GET") 
            {

                var sb = new StringBuilder();

                var interpolator = model.Parameters.Any(p => p.RouteParam) ? "$" : "";

                sb.Append($"return await client.GetFromJsonAsync<{model.ResponseType}>({interpolator}\"{model.Route}\"");

                if(model.Parameters.Count > 0)
                {
                    sb.Append(", TypeMerger.TypeMerger.Merge(");
                    sb.Append(string.Join(", ", model.Parameters.Where(p => !p.RouteParam).Select(p => p.Name)));
                    sb.Append(')');
                }

                sb.Append(");");

                body.Add(sb.ToString());
            }

            return body;
        }
    }
}
