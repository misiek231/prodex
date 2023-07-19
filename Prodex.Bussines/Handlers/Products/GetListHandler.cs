using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.Handlers.Products
{
    public class GetListHandler : BaseGetListHandler<FilterModel, ListItemModel>
    {
        private readonly DataContext context;
        public GetListHandler(DataContext context, IMapper mapper) : base(mapper)
        {
            this.context = context;
        }

        public override IQueryable<Product> GetList(FilterModel filter, CancellationToken cancellationToken)
        {
            return context.Products.Include(p => p.Template).AsQueryable();
        }
    }
}
