using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Dictionary;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class DictionaryFilter : IFilter<Dictionary, FilterModel>
{
    public IQueryable<Dictionary> Filter(IQueryable<Dictionary> query, FilterModel filterModel)
    {
        return query;
    }
}
