using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.ProductTemplates;

public class GetSelectValues
{
    public class Request : IRequest<List<KeyValueResult>>
    {
        public string Search { get; set; }
        public Pager Pager { get; set; }
    }

    public class Request<T> : Request
    {
    }

    public class SelectHandler : IRequestHandler<Request<ApiTemplateSelect>, List<KeyValueResult>>
    {
        private readonly DataContext context;

        public SelectHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<KeyValueResult>> Handle(Request<ApiTemplateSelect> request, CancellationToken cancellationToken)
        {
            var query = context.ProductTemplates.AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(p => p.Name.Contains(request.Search));

            return await query.Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}