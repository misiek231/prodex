using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Dictionary;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class DictionaryDefinition : IEndpointDefinition
{
    public string GroupName => "dictionary";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<Dictionary, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, [FromRoute] long id) => await mediator.Send(new SimpleGetDetails.Request<Dictionary, FormModel>(id)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<Dictionary, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<Dictionary, FormModel>(id, model)))
            .RequireAuthorization();
    }
}