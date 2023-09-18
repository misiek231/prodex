using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Pagination;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class ProductTemplatesDefinition : IEndpointDefinition
{
    public string GroupName => "product-templates";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<ProductTemplate, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id) => await mediator.Send(new SimpleGetDetails.Request<ProductTemplate, FormModel>(id, user.Id())))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<ProductTemplate, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<ProductTemplate, FormModel>(id, model)))
            .RequireAuthorization();
    }
}