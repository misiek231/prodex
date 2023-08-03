using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.Dictionary;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Selects;

public class SelectDictionaryHandler : IRequestHandler<SelectRequest<ApiDictionarySelect>, List<KeyValueResult>>
{
    private readonly DataContext context;

    public SelectDictionaryHandler(DataContext context)
    {
        this.context = context;
    }
    public async Task<List<KeyValueResult>> Handle(SelectRequest<ApiDictionarySelect> request, CancellationToken cancellationToken)
    {
        var query = context.Dictionaries.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(p => p.Name.Contains(request.Search));

        return await query.Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
    }
}
