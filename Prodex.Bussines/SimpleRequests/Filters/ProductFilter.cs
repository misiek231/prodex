using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class ProductFilter : IFilter<Product, FilterModel>
{
    public IQueryable<Product> Filter(IQueryable<Product> query, FilterModel filterModel)
    {
        if (filterModel.TemplateId.HasValue)
        {
            query = query.Where(p => p.TemplateId == filterModel.TemplateId.Value);
        }

        return query;
    }
}
