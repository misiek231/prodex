using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Data.Interfaces;
using Prodex.Shared.Pagination;

namespace Prodex.Bussines.SimpleRequests.Base;

public class SimpleGetList
{
    public class Request<TEntity, TFilter, TListItemModel> : IRequest<Pagination<TListItemModel>>
        where TEntity : class
    {
        public Pager Pager { get; set; }
        public TFilter Filter { get; set; }

        public Request(Pager pager, TFilter filter)
        {
            Pager = pager;
            Filter = filter;
        }
    }

    public class Handler<TEntity, TFilter, TListItemModel> : IRequestHandler<Request<TEntity, TFilter, TListItemModel>, Pagination<TListItemModel>>
        where TEntity : class, IEntity
    {
        private readonly IListMapper<TEntity, TListItemModel> Mapper;
        private readonly IFilter<TEntity, TFilter> Filter;
        private readonly DataContext Context;

        public Handler(IListMapper<TEntity, TListItemModel> mapper, DataContext context, IFilter<TEntity, TFilter> filter)
        {
            Mapper = mapper;
            Context = context;
            Filter = filter;
        }

        public async Task<Pagination<TListItemModel>> Handle(Request<TEntity, TFilter, TListItemModel> request, CancellationToken cancellationToken)
        {
            var mapped = await Context.Set<TEntity>()
                .Filter(Filter, request.Filter)
                .OrderByDescending(p => p.Id)
                .Paginate(request.Pager)
                .ToListItemModel(Mapper)
                .ToListAsync(cancellationToken);

            return new Pagination<TListItemModel>(mapped, request.Pager.TotalRows ?? 0);
        }
    }
}
