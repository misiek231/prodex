using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Bussines.Requests;
using Prodex.Bussines.Services;
using Prodex.Data;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.ProductTemplates
{
    public class SelectHandler : BaseGetSelectValuesHandler<ApiTemplateSelect>
    {

        private readonly DataContext context;
        public SelectHandler(ApiSelectCacheService apiSelectCache, DataContext context) : base(apiSelectCache)
        {
            this.context = context;
        }

        public override async Task<List<KeyValueResult>> GetValues(GetSelectValuesRequest<ApiTemplateSelect> request, CancellationToken cancellationToken)
        {
            var query = context.ProductTemplates.AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
                query = query.Where(p => p.Name.Contains(request.Search));

            return await query.Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
