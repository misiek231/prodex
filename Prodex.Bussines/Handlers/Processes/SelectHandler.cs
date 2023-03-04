using AutoMapper;
using Blazorise;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Bussines.Requests.Processes;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Pagination;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.Handlers.Processes
{
    public class SelectHandler : IRequestHandler<ProcessesSelect, Pagination<KeyValueResult>>
    {
        private readonly DataContext context;
        public SelectHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
        }

        public async Task<Pagination<KeyValueResult>> Handle(ProcessesSelect request, CancellationToken cancellationToken)
        {
            var items = await context.Processes
                .Where(p => p.Name.Contains(request.Search))
                .Select(p => new KeyValueResult(p.Id, p.Name)).ToListAsync();

            return new Pagination<KeyValueResult>(items, items.Count); 
        }
    }
}
