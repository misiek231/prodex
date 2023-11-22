using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Dictionary;

namespace Prodex.Server.Controllers;

public class DictionaryDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "dictionary";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        DefineGetList<Dictionary, FilterModel, ListItemModel>(group);
        DefineGetDetails<Dictionary, FormModel>(group);
        DefineCreate<Dictionary, FormModel>(group);
        DefineUpdate<Dictionary, FormModel>(group);
    }
}