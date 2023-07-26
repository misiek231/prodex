using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Pagination;
using Prodex.Shared.Models.ProductTemplates.Statuses;
using Prodex.Bussines.Requests.ProductTemplates.Statuses;

namespace Prodex.Server.Controllers;

public class PtStatusesDefinition : IEndpointDefinition
{
    public string GroupName => "pt-statuses";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) => 
            await mediator.Send(new GetListRequest<FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, [FromRoute] long id) => await mediator.Send(new GetDetails(id)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new CreateRequest<FormModel, object>(model)))
            .RequireAuthorization();

        
        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new UpdateRequest<FormModel, object>(id, model)))
            .RequireAuthorization();

    }
}