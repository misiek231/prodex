using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;

namespace Prodex.Bussines.SimpleRequests.Filters
{
    public class HistoryFilterModel
    {
        public long ProductId { get; set; }
    }

    internal class HistoryFilter : IFilter<History, HistoryFilterModel>
    {
        public IQueryable<History> Filter(IQueryable<History> query, HistoryFilterModel filterModel)
        {
            return query.Where(p => p.ProductId == filterModel.ProductId);
        }
    }
}
