using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Handlers.Selects;
using Prodex.Bussines.Services;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class ApiSelectDefinition : IEndpointDefinition
{
    public string GroupName => "api-select";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("api-field-config", async (IMediator mediator, [FromQuery] long templateId, [AsParameters] Pager pager, [FromQuery] string search) =>
        {
            var request = new SelectDynamicFieldRequest
            {
                TemplateId = templateId,
                Pager = pager,
                Search = search
            };

            return await mediator.Send(request);

        }).RequireAuthorization();

        group.MapGet("api-status-select", async (IMediator mediator, [FromQuery] long templateId, [AsParameters] Pager pager, [FromQuery] string search) =>
        {
            var request = new SelectStatusRequest
            {
                TemplateId = templateId,
                Pager = pager,
                Search = search
            };

            return await mediator.Send(request);

        }).RequireAuthorization();

        group.MapGet("{name}", async (IMediator mediator, ApiSelectCacheService selectCache, [FromRoute] string name, [AsParameters] Pager pager, [FromQuery] string search) =>
        {
            var request = selectCache.GetRequestByName(name);

            request.Search = search;
            request.Pager = pager;

            return await mediator.Send(request);

        }).RequireAuthorization();
    }
}