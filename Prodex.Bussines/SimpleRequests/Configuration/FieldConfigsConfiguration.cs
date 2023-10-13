using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class FieldConfigsConfiguration : IConfigurator
{
    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<FieldConfig, FilterModel, FieldModel>();
    }
}
