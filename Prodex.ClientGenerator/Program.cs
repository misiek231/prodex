using Microsoft.AspNetCore.Routing;
using Prodex.ClientGenerator;
using Prodex.Server;

var endpointRouteBuilder = App.CreateApp(args) as IEndpointRouteBuilder;
var routes = endpointRouteBuilder.DataSources.First();
new ClientGenerator().Generate(routes);
