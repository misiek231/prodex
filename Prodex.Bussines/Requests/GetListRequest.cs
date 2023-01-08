using MediatR;
using Prodex.Shared.Pagination;

namespace Prodex.Server.Requests
{
    public class GetListRequest<TFilter, TItem> : IRequest<Pagination<TItem>>
    {
        public GetListRequest(Pager pager, TFilter filter)
        {
            Pager = pager;
            Filter = filter;
        }

        public Pager Pager { get; set; }
        public TFilter Filter { get; set; }
    }
}
