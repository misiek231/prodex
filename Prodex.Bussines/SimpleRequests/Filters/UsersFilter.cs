using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class UsersFilter : IFilter<User, FilterModel>
{
    public IQueryable<User> Filter(IQueryable<User> query, FilterModel filterModel)
    {
        return query;
    }
}
