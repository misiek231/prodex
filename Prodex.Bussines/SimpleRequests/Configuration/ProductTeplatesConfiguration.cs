using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class ProductTeplatesConfiguration : IConfigurator
{

    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<ProductTemplate, FilterModel, ListItemModel>()
            .AddCreateConfig<ProductTemplate, FormModel>()
            .AddUpdateConfig<ProductTemplate, FormModel>()
            .AddDetailsConfig<ProductTemplate, FormModel>();
    }

}
