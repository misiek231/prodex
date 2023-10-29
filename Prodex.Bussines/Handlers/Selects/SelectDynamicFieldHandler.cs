using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Selects;


public class SelectDynamicFieldHandlerHandler : IRequestHandler<SelectDynamicFieldRequest, List<KeyValueResult>>
{
    private readonly DataContext context;

    public SelectDynamicFieldHandlerHandler(DataContext context)
    {
        this.context = context;
    }
    public async Task<List<KeyValueResult>> Handle(SelectDynamicFieldRequest request, CancellationToken cancellationToken)
    {
        var query = context.FieldConfigs.Where(p => p.TemplateId == request.TemplateId).AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(p => p.DisplayName.Contains(request.Search));

        return await query.Select(p => new KeyValueResult(p.Id, p.DisplayName)).ToListAsync(cancellationToken: cancellationToken);
    }
}
