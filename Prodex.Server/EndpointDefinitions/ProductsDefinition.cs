using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Filters;
using Prodex.Data.Models;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Pagination;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class ProductsDefinition : IEndpointDefinition
{
    public string GroupName => "products";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] Pager pager, [AsParameters] FilterModel model) =>
            await mediator.Send(new SimpleGetList.Request<Product, FilterModel, ListItemModel>(pager, model)))
            .RequireAuthorization();

        group.MapGet("{productId}/history", async (IMediator mediator, [AsParameters] Pager pager, [FromRoute] long productId) =>
            await mediator.Send(new SimpleGetList.Request<History, HistoryFilterModel, Shared.Models.Products.History.ListItemModel>(pager, new HistoryFilterModel { ProductId = productId })))
            .WithDisplayName("History")
            .RequireAuthorization();

        group.MapGet("{id}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long id) =>
            await mediator.Send(new SimpleGetDetails.Request<Product, DetailsModel>(id, user.Id())))
            .RequireAuthorization();

        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(new SimpleCreate.Request<Product, FormModel>(model)))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] FormModel model) => await mediator.Send(new SimpleUpdate.Request<Product, FormModel>(id, model)))
            .RequireAuthorization();

        group.MapPost("execute/{productId}/{stepId}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long productId, [FromRoute] long stepId) =>
            await mediator.Send(new ExecuteStep.Request(productId, stepId, user.Id())))
            .RequireAuthorization();
    }
}