using Microsoft.AspNetCore.Routing;
using Prodex.ClientGenerator;
using Prodex.Server;

var endpointRouteBuilder = Application.CreateApp(args) as IEndpointRouteBuilder;
var routes = endpointRouteBuilder.DataSources.First();
new ClientGenerator().Generate(routes);
