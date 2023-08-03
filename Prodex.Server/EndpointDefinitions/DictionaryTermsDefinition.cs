using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.DictionaryTerms;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class DictionaryTermsDefinition : IEndpointDefinition
{
    public string GroupName => "dictionary-terms";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<DictionaryTerm, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, [FromRoute] long id) => await mediator.Send(new SimpleGetDetails.Request<DictionaryTerm, FormModel>(id)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<DictionaryTerm, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<DictionaryTerm, FormModel>(id, model)))
            .RequireAuthorization();
    }
}