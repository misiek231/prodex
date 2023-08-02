using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class PtStatusesConfiguration : IConfigurator
{

    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<PtStatus, FilterModel, ListItemModel>()
            .AddCreateConfig<PtStatus, FormModel>()
            .AddUpdateConfig<PtStatus, FormModel>()
            .AddDetailsConfig<PtStatus, FormModel>();
    }

}
