using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Filters;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products.History;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class HistoryConfiguration : IConfigurator
{

    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<History, HistoryFilterModel, ListItemModel>();
    }
}
