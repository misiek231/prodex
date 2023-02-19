using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodex.Bussines.Requests;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Auth;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Controllers;

public class AuthDefinition : IEndpointDefinition
{
    public string GroupName => "auth";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapPost("login", async (IMediator mediator, [FromBody] LoginModel model) => await mediator.Send(new CreateRequest<LoginModel, TokenModel>(model)));
    }
}