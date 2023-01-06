using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Processes;

namespace Prodex.Server.Controllers;

public class ProcessesDefinition : IEndpointDefinition
{
    public string GroupName => "processes";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, [AsParameters] FilterModel model) => await mediator.Send(model));
        group.MapPost("", async (IMediator mediator, [FromBody] FormModel model) => await mediator.Send(model));
    }
}