using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Selects;

public class SelectUserHandler : IRequestHandler<SelectRequest<ApiUserSelect>, List<KeyValueResult>>
{
    private readonly DataContext context;

    public SelectUserHandler(DataContext context)
    {
        this.context = context;
    }
    public async Task<List<KeyValueResult>> Handle(SelectRequest<ApiUserSelect> request, CancellationToken cancellationToken)
    {
        var query = context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(p => p.GivenName.Contains(request.Search) || p.Surname.Contains(request.Search));

        return await query.Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
    }
}
