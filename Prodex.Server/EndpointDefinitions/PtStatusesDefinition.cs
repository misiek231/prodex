using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates.Statuses;
using Prodex.Shared.Pagination;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class PtStatusesDefinition : IEndpointDefinition
{
    public string GroupName => "pt-statuses";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<PtStatus, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id) => await mediator.Send(new SimpleGetDetails.Request<PtStatus, FormModel>(id, user.Id())))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<PtStatus, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<PtStatus, FormModel>(id, model)))
            .RequireAuthorization();
    }
}