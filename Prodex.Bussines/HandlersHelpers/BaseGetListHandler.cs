using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Server.Requests;
using Prodex.Shared.Pagination;

namespace Prodex.Bussines.HandlersHelpers;

public abstract class BaseGetListHandler<TFilter, TItem> : IRequestHandler<GetListRequest<TFilter, TItem>, Pagination<TItem>>
{
    public async Task<Pagination<TItem>> Handle(GetListRequest<TFilter, TItem> request, CancellationToken cancellationToken)
    {
        return new Pagination<TItem>(await GetList(request.Filter, cancellationToken).Paginate(request.Pager).ToListAsync(cancellationToken));
    }

    public abstract IQueryable<TItem> GetList(TFilter filter, CancellationToken cancellationToken);

}
