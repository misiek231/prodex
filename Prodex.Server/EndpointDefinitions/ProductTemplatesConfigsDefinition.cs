using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class ProductTemplatesConfigsDefinition : IEndpointDefinition
{
    public string GroupName => "product-templates-configs";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId) => 
        await mediator.Send(new GetServiceTaskDetails.Request(templateId, stepId)))
            .RequireAuthorization();

        group.MapPost("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId,  [FromBody] ServiceTaskConfigFormModel model) => 
            await mediator.Send(new SimpleCreate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(new ServiceTaskConfigFormModelExtended(model, templateId, stepId))))
            .RequireAuthorization();

        group.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] ServiceTaskConfigFormModel model) => 
            await mediator.Send(new SimpleUpdate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(id, new ServiceTaskConfigFormModelExtended(model))))
            .RequireAuthorization();
    }
}