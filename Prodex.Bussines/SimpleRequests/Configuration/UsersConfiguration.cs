using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.SimpleRequests.Configuration;

public class UsersConfiguration : IConfigurator
{
    public void Configure(SimpleRequestConfig config)
    {
        config.AddListConfig<User, FilterModel, ListItemModel>()
            .AddCreateConfig<User, FormModel>()
            .AddUpdateConfig<User, FormModel>()
            .AddDetailsConfig<User, FormModel>();
    }
}
