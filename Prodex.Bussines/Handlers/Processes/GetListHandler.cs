using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
{
    public class GetListHandler : BaseGetListHandler<FilterModel, ListItemModel>
    {
        private readonly DataContext context;
        public GetListHandler(DataContext context)
        {
            this.context = context;
        }

        public override IQueryable<ListItemModel> GetList(FilterModel filter, CancellationToken cancellationToken)
        {
            return context.Processes.Select(p => new ListItemModel
            {
                Id = p.Id,
                Name = p.Name,
                Xml = p.Xml,
            });
        }
    }
}
