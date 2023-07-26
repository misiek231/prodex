using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.Handlers.ProductTemplates.Statuses;

public class GetListHandler : BaseGetListHandler<FilterModel, ListItemModel>
{
    private readonly DataContext context;
    public GetListHandler(DataContext context, IMapper mapper) : base(mapper)
    {
        this.context = context;
    }

    public override IQueryable<PtStatus> GetList(FilterModel filter, CancellationToken cancellationToken)
    {
        return context.PtStatuses.Where(p => p.TemplateId == filter.TemplateId).AsQueryable();
    }
}
