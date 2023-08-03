using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Dictionary;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class DictionaryConfiguration : IConfigurator
{
    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<Dictionary, FilterModel, ListItemModel>()
            .AddCreateConfig<Dictionary, FormModel>()
            .AddUpdateConfig<Dictionary, FormModel>()
            .AddDetailsConfig<Dictionary, FormModel>();
    }
}
