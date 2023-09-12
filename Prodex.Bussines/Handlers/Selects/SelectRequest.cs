using MediatR;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Selects;

public class SelectRequest : IRequest<List<KeyValueResult>>
{
    public string Search { get; set; }
    public Pager Pager { get; set; }
}

public class SelectRequest<T> : SelectRequest
{
}

public class SelectStatusRequest : SelectRequest<ApiStatus>
{
    public long TemplateId { get; set; }
}

