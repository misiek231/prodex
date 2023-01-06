using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
{
    public class GetListHandler : IRequestHandler<FilterModel, List<ListItemModel>>
    {
        private readonly DataContext context;
        public GetListHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<ListItemModel>> Handle(FilterModel request, CancellationToken cancellationToken)
        {
            return await context.Processes.Select(p => new ListItemModel
            {
                Id = p.Id,
                Name = p.Name,
                Xml = p.Xml,
            }).ToListAsync(cancellationToken);
        }
    }
}
