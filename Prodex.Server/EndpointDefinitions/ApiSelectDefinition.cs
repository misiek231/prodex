using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Bussines.Services;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Auth;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Pagination;
using System.Reflection;

namespace Prodex.Server.Controllers;

public class ApiSelectDefinition : IEndpointDefinition
{
    public string GroupName => "api-select";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("{name}", async (IMediator mediator, ApiSelectCacheService selectCache, [FromRoute] string name, [AsParameters] Pager pager, [FromQuery] string search) => {

            var request = selectCache.GetRequestByName(name);

            request.Search = search;
            request.Pager = pager;
            
            return await mediator.Send(request);

        }).RequireAuthorization();
    }
}