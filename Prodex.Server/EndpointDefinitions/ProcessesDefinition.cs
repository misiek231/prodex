using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Bussines.Requests.Processes;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class ProcessesDefinition : IEndpointDefinition
{
    public string GroupName => "processes";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) => 
            await mediator.Send(new GetListRequest<FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("select", async (IMediator mediator, [AsParameters] Pager pager, [FromQuery] string search) =>
            await mediator.Send(new ProcessesSelect(pager, search)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new CreateRequest<FormModel, object>(model)))
            .RequireAuthorization();
    }
}