using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.DictionaryTerms;

namespace Prodex.Server.Controllers;

public class DictionaryTermsDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "dictionary-terms";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        group.RequireAuthorization(Shared.Models.Users.UserType.Admin.ToString());

        DefineGetList<DictionaryTerm, FilterModel, ListItemModel>(group);
        DefineGetDetails<DictionaryTerm, FormModel>(group);
        DefineCreate<DictionaryTerm, FormModel>(group);
        DefineUpdate<DictionaryTerm, FormModel>(group);
    }
}