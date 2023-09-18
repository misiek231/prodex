using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Pagination;
using System.Security.Claims;
using System.Security.Principal;

namespace Prodex.Server.Controllers;

public class UsersDefinition : IEndpointDefinition
{
    public string GroupName => "users";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<User, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id) => await mediator.Send(new SimpleGetDetails.Request<User, FormModel>(id, user.Id())))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<User, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<User, FormModel>(id, model)))
            .RequireAuthorization();
    }
}