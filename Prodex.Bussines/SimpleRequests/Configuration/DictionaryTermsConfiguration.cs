using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.DictionaryTerms;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class DictionaryTermsDictionaryConfiguration : IConfigurator
{
    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<DictionaryTerm, FilterModel, ListItemModel>()
            .AddCreateConfig<DictionaryTerm, FormModel>()
            .AddUpdateConfig<DictionaryTerm, FormModel>()
            .AddDetailsConfig<DictionaryTerm, FormModel>();
    }
}
