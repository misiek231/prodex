using MediatR;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Requests
{

    public class GetSelectValuesRequest : IRequest<List<KeyValueResult>>
    {
        public string Search { get; set; }
        public Pager Pager { get; set; }
    }

    public class GetSelectValuesRequest<T> : GetSelectValuesRequest
    {
        
    }
}
