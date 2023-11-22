using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.ProductTemplatesConfigs;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class FieldsConfigDefinition : IEndpointDefinition
{
    public string GroupName => "fields";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        group.MapGet("{templateId:long}", async (IMediator mediator, [FromRoute] long templateId, [AsParameters] Pager pager) =>
            await mediator.Send(new SimpleGetList.Request<FieldConfig, FilterModel, FieldModel>(pager, new FilterModel { TemplateId = templateId }))
        ).RequireAuthorization();

        group.MapPost("{templateId:long}", async (IMediator mediator, long templateId, [FromBody] FieldsConfigFormModel model) =>
            await mediator.Send(new UpdateFieldsConfigHandler.Request(templateId, model))
        ).RequireAuthorization();        
    }
}