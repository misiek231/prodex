using Prodex.Bussines.Handlers.Products;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class ProductsConfiguration : IConfigurator
{

    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<Product, FilterModel, ListItemModel>()
            .AddCreateConfig<Product, FormModel, RunProcess.Command>()
            .AddUpdateConfig<Product, FormModel>()
            .AddDetailsConfig<Product, DetailsModel, GetProductDetailsHandler>();
    }
}
