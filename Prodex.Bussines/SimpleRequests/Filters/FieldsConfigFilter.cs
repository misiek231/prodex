using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class FieldsConfigFilter : IFilter<FieldConfig, FilterModel>
{
    public IQueryable<FieldConfig> Filter(IQueryable<FieldConfig> query, FilterModel filterModel)
    {
        return query.Where(p => p.TemplateId == filterModel.TemplateId);
    }
}
