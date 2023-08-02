using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class ProductTemplateFilter : IFilter<ProductTemplate, FilterModel>
{
    public IQueryable<ProductTemplate> Filter(IQueryable<ProductTemplate> query, FilterModel filterModel)
    {
        return query;
    }
}
