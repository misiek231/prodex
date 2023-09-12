using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Selects;


public class SelectStatusHandler : IRequestHandler<SelectStatusRequest, List<KeyValueResult>>
{
    private readonly DataContext context;

    public SelectStatusHandler(DataContext context)
    {
        this.context = context;
    }
    public async Task<List<KeyValueResult>> Handle(SelectStatusRequest request, CancellationToken cancellationToken)
    {
        var query = context.PtStatuses.Where(p => p.TemplateId == request.TemplateId).AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(p => p.Name.Contains(request.Search));

        return await query.Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
    }
}
