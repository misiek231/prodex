using Prodex.Data.Models;
using Prodex.Server.EndpointDefinitions;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Server.Controllers;

public class ProductTemplatesDefinition : SimpleRequestsBaseDefinition, IEndpointDefinition
{
    public string GroupName => "product-templates";

    public void DefineEndpoints(RouteGroupBuilder group)
    {
        DefineGetList<ProductTemplate, FilterModel, ListItemModel>(group);
        DefineGetDetails<ProductTemplate, FormModel>(group);
        DefineCreate<ProductTemplate, FormModel>(group);
        DefineUpdate<ProductTemplate, FormModel>(group);
    }
}