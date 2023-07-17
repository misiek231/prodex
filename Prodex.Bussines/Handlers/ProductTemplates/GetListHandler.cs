using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.Handlers.ProductTemplates
{
    public class GetListHandler : BaseGetListHandler<FilterModel, ListItemModel>
    {
        private readonly DataContext context;
        public GetListHandler(DataContext context, IMapper mapper) : base(mapper)
        {
            this.context = context;
        }

        public override IQueryable<ProductTemplate> GetList(FilterModel filter, CancellationToken cancellationToken)
        {
            return context.ProductTemplates.AsQueryable();
        }
    }
}
