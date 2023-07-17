using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.Handlers.Products
{
    public class CreateHandler : BaseCreateHandler<FormModel, object>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CreateHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public override async Task<object> Create(FormModel form, CancellationToken cancellationToken)
        {
            context.ProductTemplates.Add(mapper.Map<ProductTemplate>(form));
            await context.SaveChangesAsync(cancellationToken);
            return null; // Todo: return detils model
        }
    }
}
