using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Products.DynamicFields;

namespace Prodex.Server.Controllers;

public class DynamicFieldsDetailsDefinition : IEndpointDefinition
{
    public string GroupName => "fields-details";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("{productId:long}", async (IMediator mediator, [FromRoute] long productId) =>
            await mediator.Send(new GetProductFieldsDetails.Request(productId))
        ).RequireAuthorization();

        group.MapPost("{productId:long}/{fieldConfigId:long}", async (IMediator mediator, long productId, long fieldConfigId, SetProductFieldFormModel model) =>
            await mediator.Send(new SetProductFieldValue.Request(productId, fieldConfigId, model.NewValue))
        ).RequireAuthorization(); 
    }
}