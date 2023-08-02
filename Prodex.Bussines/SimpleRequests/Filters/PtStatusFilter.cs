using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.SimpleRequests.Filters;

public class PtStatusFilter : IFilter<PtStatus, FilterModel>
{
    public IQueryable<PtStatus> Filter(IQueryable<PtStatus> query, FilterModel filterModel)
    {
        query = query.Where(p => p.TemplateId == filterModel.TemplateId);

        return query;
    }
}
