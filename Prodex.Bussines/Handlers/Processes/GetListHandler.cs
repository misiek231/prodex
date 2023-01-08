using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
{
    public class GetListHandler : BaseGetListHandler<FilterModel, ListItemModel>
    {
        private readonly DataContext context;
        public GetListHandler(DataContext context, IMapper mapper) : base(mapper)
        {
            this.context = context;
        }

        public override IQueryable<Process> GetList(FilterModel filter, CancellationToken cancellationToken)
        {
            return context.Processes.AsQueryable();
        }
    }
}
