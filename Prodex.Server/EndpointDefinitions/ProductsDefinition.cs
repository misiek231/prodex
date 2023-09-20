using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Filters;
using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Pagination;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class ProductsDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "products";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        DefineGetList<Product, FilterModel, ListItemModel>(group);
        DefineGetDetails<Product, DetailsModel>(group);
        DefineCreate<Product, FormModel>(group);
        DefineUpdate<Product, FormModel>(group);

        group.MapGet("{productId}/history", async (IMediator mediator, [AsParameters] Pager pager, [FromRoute] long productId) =>
            await mediator.Send(new SimpleGetList.Request<History, HistoryFilterModel, Shared.Models.Products.History.ListItemModel>(pager, new HistoryFilterModel { ProductId = productId })))
            .WithDisplayName("History")
            .RequireAuthorization();

        group.MapPost("execute/{productId}/{stepId}", async (IMediator mediator, ClaimsPrincipal user, [FromRoute] long productId, [FromRoute] long stepId) =>
            await mediator.Send(new ExecuteStep.Request(productId, stepId, user.Id())))
            .RequireAuthorization();
    }
}