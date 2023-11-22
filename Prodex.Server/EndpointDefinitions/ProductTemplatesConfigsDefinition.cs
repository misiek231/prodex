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
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        DefineServiceTask(group);
        DefineSendTask(group);
        DefineSequenceFlow(group);
    }

    private static void DefineSendTask(RouteGroupBuilder group)
    {
        var sendTask = group.MapGroup("send-task");

        sendTask.MapGet("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId) =>
        await mediator.Send(new GetSendTaskDetails.Request(templateId, stepId)))
            .WithDisplayName("GetSendTaskConfig")
            .RequireAuthorization();

        sendTask.MapPost("{templateId}/{stepId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string stepId, [FromBody] SendTaskConfigFormModel model) =>
            {
                var result = await mediator.Send(new SimpleCreate.Request<SendTaskConfig, SendTaskConfigFormModelExtended>(new SendTaskConfigFormModelExtended(model, templateId, stepId)));

                return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
            })
            .WithDisplayName("CreateSendTaskConfig")
            .RequireAuthorization();

        sendTask.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] SendTaskConfigFormModel model) =>
            {
                var result = await mediator.Send(new SimpleUpdate.Request<SendTaskConfig, SendTaskConfigFormModelExtended>(id, new SendTaskConfigFormModelExtended(model)));
                return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
            })
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
        {
            var result = await mediator.Send(new SimpleCreate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(new ServiceTaskConfigFormModelExtended(model, templateId, stepId)));
            return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
        })
        .WithDisplayName("CreateServiceTaskConfig")
        .RequireAuthorization();

        serviceTask.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] ServiceTaskConfigFormModel model) =>
        {
            var result = await mediator.Send(new SimpleUpdate.Request<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>(id, new ServiceTaskConfigFormModelExtended(model)));
            return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
        })
        .WithDisplayName("UpdateServiceTaskConfig")
        .RequireAuthorization();
    }

   private static void DefineSequenceFlow(RouteGroupBuilder group)
   {
        var sequenceFlow = group.MapGroup("sequence-flow");

        sequenceFlow.MapGet("{templateId}/{flowId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string flowId) =>
        await mediator.Send(new GetSequenceFlowDetails.Request(templateId, flowId)))
            .WithDisplayName("GetSequenceFlowConfig")
            .RequireAuthorization();

        sequenceFlow.MapPost("{templateId}/{flowId}", async (IMediator mediator, [FromRoute] long templateId, [FromRoute] string flowId, [FromBody] SequenceFlowConfigFormModel model) =>
        {
            var result = await mediator.Send(new SimpleCreate.Request<SequenceFlowConfig, SequenceFlowConfigFormModelExtended>(new SequenceFlowConfigFormModelExtended(model, templateId, flowId)));
            return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
        })
        .WithDisplayName("CreateSequenceFlowConfig")
        .RequireAuthorization();

        sequenceFlow.MapPut("{id}", async (IMediator mediator, [FromRoute] long id, [FromBody] SequenceFlowConfigFormModel model) =>
        {
            var result = await mediator.Send(new SimpleUpdate.Request<SequenceFlowConfig, SequenceFlowConfigFormModelExtended>(id, new SequenceFlowConfigFormModelExtended(model)));
            return result.Match(p => Results.Ok(p), p => Results.BadRequest(p));
        })
        .WithDisplayName("UpdateSequenceFlowConfig")
        .RequireAuthorization();
   }
}