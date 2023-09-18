using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.Handlers.ProductTemplatesConfigs;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Server.Controllers;

public class ProductTemplatesConfigsDefinition : IEndpointDefinition
{
    public string GroupName => "product-templates-configs";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        DefineServiceTask(group);
        DefineSendTask(group);
    }

    private static void DefineSendTask(RouteGroupBuilder group)
    {
        var sendTask = group.MapGroup("send-task");

        sendTask.MapGet("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId) =>
        await mediator.Send(new GetSendTaskDetails.Request(templateId, stepId)))
            .WithDisplayName("GetSendTaskConfig")
            .RequireAuthorization();

        sendTask.MapPost("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId, [FromBody] SendTaskConfigFormModel model) =>
            await mediator.Send(new SimpleCreate.Request<SendTaskConfig, SendTaskConfigFormModelExtended>(new SendTaskConfigFormModelExtended(model, templateId, stepId))))
            .WithDisplayName("CreateSendTaskConfig")
            .RequireAuthorization();

        sendTask.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] SendTaskConfigFormModel model) =>
            await mediator.Send(new SimpleUpdate.Request<SendTaskConfig, SendTaskConfigFormModelExtended>(id, new SendTaskConfigFormModelExtended(model))))
            .WithDisplayName("UpdateSendTaskConfig")
            .RequireAuthorization();
    }

    private static void DefineServiceTask(RouteGroupBuilder group)
    {
        var serviceTask = group.MapGroup("service-task");

        serviceTask.MapGet("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId) =>
        await mediator.Send(new GetServiceTaskDetails.Request(templateId, stepId)))
            .WithDisplayName("GetServiceTaskConfig")
            .RequireAuthorization();

        serviceTask.MapPost("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId, [FromBody] ServiceTaskConfigFormModel model) =>
            await mediator.Send(new SimpleCreate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(new ServiceTaskConfigFormModelExtended(model, templateId, stepId))))
            .WithDisplayName("CreateServiceTaskConfig")
            .RequireAuthorization();

        serviceTask.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] ServiceTaskConfigFormModel model) =>
            await mediator.Send(new SimpleUpdate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(id, new ServiceTaskConfigFormModelExtended(model))))
            .WithDisplayName("UpdateServiceTaskConfig")
            .RequireAuthorization();
    }
}