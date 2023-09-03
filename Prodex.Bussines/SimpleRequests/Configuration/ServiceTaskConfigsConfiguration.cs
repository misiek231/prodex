using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class ServiceTaskConfigsConfiguration : IConfigurator
{
    public void Configure(SimpleRequestConfig config)
    {
        config.AddCreateConfig<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>()
            .AddUpdateConfig<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>();
    }
}
