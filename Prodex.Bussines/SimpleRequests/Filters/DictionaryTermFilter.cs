using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.DictionaryTerms;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class DictionaryTermsFilter : IFilter<DictionaryTerm, FilterModel>
{
    public IQueryable<DictionaryTerm> Filter(IQueryable<DictionaryTerm> query, FilterModel filterModel)
    {
        if (filterModel.DictionaryId != 0)
            query = query.Where(p => p.DictionaryId == filterModel.DictionaryId);

        return query;
    }
}
