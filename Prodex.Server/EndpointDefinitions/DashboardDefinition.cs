using MediatR;
using Prodex.Bussines.Handlers.Dashboard;
using Prodex.Server.Extensions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Users;
using System.Security.Claims;

namespace Prodex.Server.Controllers;

public class DashboardDefinition : IEndpointDefinition
{
    public string GroupName => "dashboard";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("", async (IMediator mediator, ClaimsPrincipal user) =>
        {
            return await mediator.Send(new DashboardHandler.Request(user.Id(), user.HasRole(UserType.Admin)));
        }).RequireAuthorization();
    }
}