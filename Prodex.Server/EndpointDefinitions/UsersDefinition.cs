using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Users;

namespace Prodex.Server.Controllers;

public class UsersDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "users";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        DefineGetList<User, FilterModel, ListItemModel>(group);
        DefineGetDetails<User, FormModel>(group);
        DefineCreate<User, FormModel>(group);
        DefineUpdate<User, FormModel>(group);
    }
}