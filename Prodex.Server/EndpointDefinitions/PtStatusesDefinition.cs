using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Server.Controllers;

public class PtStatusesDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "pt-statuses";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        DefineGetList<PtStatus, FilterModel, ListItemModel>(group);
        DefineGetDetails<PtStatus, FormModel>(group);
        DefineCreate<PtStatus, FormModel>(group);
        DefineUpdate<PtStatus, FormModel>(group);
    }
}