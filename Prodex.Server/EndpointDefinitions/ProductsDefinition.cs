using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class ProductsDefinition : IEndpointDefinition
{
    public string GroupName => "products";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<Product, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, [FromRoute] long id) =>
            await mediator.Send(new SimpleGetDetails.Request<Product, DetailsModel>(id)))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<Product, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<Product, FormModel>(id, model)))
            .RequireAuthorization();

        group.MapPost("execute/{productId}/{stepId}", async (IMediator mediator, [FromRoute] long productId, [FromRoute] long stepId) =>
            await mediator.Send(new ExecuteStep.Request(productId, stepId)))
            .RequireAuthorization();
    }
}