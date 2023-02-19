using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class UsersDefinition : IEndpointDefinition
{
    public string GroupName => "users";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) => 
            await mediator.Send(new GetListRequest<FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new CreateRequest<FormModel, object>(model)))
            .RequireAuthorization();
    }
}