using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Requests;
using Prodex.Shared.Pagination;

namespace Prodex.Bussines.HandlersHelpers;

public abstract class BaseGetListHandler<TFilter, TItem> : IRequestHandler<GetListRequest<TFilter, TItem>, Pagination<TItem>>
{

    protected readonly IMapper mapper;

    protected BaseGetListHandler(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<Pagination<TItem>> Handle(GetListRequest<TFilter, TItem> request, CancellationToken cancellationToken)
    {
        var list = await GetList(request.Filter, cancellationToken).Paginate(request.Pager).ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<TItem>>(list);
        return new Pagination<TItem>(mapped, request.Pager.TotalRows ?? 0);
    }

    public abstract IQueryable<object> GetList(TFilter filter, CancellationToken cancellationToken);

}
