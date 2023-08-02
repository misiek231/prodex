using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Auth;
using Prodex.Bussines.Sitemap;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Auth;
using System.Reflection;

namespace Prodex.Server.Controllers;

public class AuthDefinition : IEndpointDefinition
{
    public string GroupName => "auth";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("sitemap", async (IMediator mediator) =>
        {

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            path = Path.Combine(path, "sitemap.json");

            return await mediator.Send(new GetSitemap.Request(path));
        }).RequireAuthorization();

        group.MapPost("login", async (IMediator mediator, [FromBody] LoginModel model) =>
            await mediator.Send(new Login.Request(model)));
    }
}