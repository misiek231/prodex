using Microsoft.AspNetCore.Routing;
using Prodex.Shared.Forms;
using Prodex.Shared.Models.Processes;
using System.Reflection;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prodex.ClientGenerator
{
    public class ClientModel
    {
        public string Name { get; set; }
        public List<EndpointModel> Endpoints { get; set; }

        public ClientModel(IGrouping<string, RouteEndpoint> group) 
        {
            Name = ConvertKebabCaseToPascalCase(group.Key);
            Endpoints = group.Select(item => new EndpointModel(item)).ToList();
        }

        public string ConvertKebabCaseToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string pascalCaseString = Regex.Replace(input, "(^|-)([a-z])", match =>
            {
                string letter = match.Groups[2].Value;
                return letter.ToUpper();
            });

            return pascalCaseString;
        }
    }

    public class EndpointModel
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public string MethodName { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Method { get; set; }
        public string Route { get; set; }
        public string ResponseType { get; set; }
        public bool Validate { get; set; }

        public EndpointModel(RouteEndpoint item)
        {
            var methodInfo = item.Metadata.GetRequiredMetadata<MethodInfo>();
            var name = item.RoutePattern.RawText.Split("/").ElementAtOrDefault(2);
            name = !string.IsNullOrEmpty(name) ? name.FirstLetterUpper() : null;
            var method = item.Metadata.GetRequiredMetadata<HttpMethodMetadata>().HttpMethods[0];
            var parameteters = item.Metadata.GetRequiredMetadata<MethodInfo>().GetParameters();

            Method = method;
            MethodName = name ?? method.FirstLetterUpper();
            Route = item.RoutePattern.RawText;
            ResponseType = methodInfo.ReturnParameter.ToString().FixTypeDefinition().RemoveTask();
            ReturnType = method == "POST" ? $"System.Threading.Tasks.Task<Prodex.Client.RestClients.HttpResponseMessage<{ResponseType}>>" : methodInfo.ReturnParameter.ToString().FixTypeDefinition();
            Parameters = parameteters.Where(p => p.ParameterType.Assembly == typeof(FilterModel).Assembly)
                    .Select(p => new Parameter(p)).ToList();
            Validate = parameteters.Where(p => p.ParameterType.IsAssignableTo(typeof(FormBaseModel))).Any();
        }
    }

    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public Parameter(ParameterInfo param)
        {
            Name = param.Name;
            Type = param.ParameterType.FullName;
        }
    }
}
