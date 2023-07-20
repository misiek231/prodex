using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.Handlers.ProductTemplates
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

        public override async Task<object> Update(long id, FormModel form, CancellationToken cancellationToken)
        {
            var entity = await context.ProductTemplates.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            mapper.Map(form, entity);
            context.SaveChanges();

            return null;
        }
    }
}
