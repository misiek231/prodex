using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Auth;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Pagination;
using System.Reflection;

namespace Prodex.Server.Controllers;

public class AuthDefinition : IEndpointDefinition
{
    public string GroupName => "auth";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("sitemap", async (IMediator mediator) => {

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            path = Path.Combine(path, "sitemap.json");

            return await mediator.Send(new GetSitemapRequest(path));
        }).RequireAuthorization();
        group.MapPost("login", async (IMediator mediator, [FromBody] LoginModel model) => await mediator.Send(new CreateRequest<LoginModel, TokenModel>(model)));
    }
}