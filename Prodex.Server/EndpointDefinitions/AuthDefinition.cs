using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Auth;
using Prodex.Bussines.Sitemap;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Auth;
using Prodex.Shared.Models.Users;
using System.Reflection;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class AuthDefinition : IEndpointDefinition
{
    public string GroupName => "auth";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("sitemap", async (IMediator mediator, ClaimsPrincipal user) =>
        {
            return await mediator.Send(new GetSitemap.Request(user.Id()));
        }).RequireAuthorization();

        group.MapPost("login", async (IMediator mediator, [FromBody] LoginModel model) =>
            (await mediator.Send(new Login.Request(model)))
                .Match(p => Results.Ok(p), p => Results.UnprocessableEntity(p.Value))
            );
    }
}