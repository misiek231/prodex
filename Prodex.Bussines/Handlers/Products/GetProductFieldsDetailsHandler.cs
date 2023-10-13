using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Shared.Models.Products.DynamicFields;

namespace Prodex.Bussines.Handlers.Products;

public static class GetProductFieldsDetails
{
    public class Request : IRequest<List<DynamicFieldDetailsModel>>
    {
        public long ProductId { get; set; }

        public Request(long productId)
        {
            ProductId = productId;
        }
    }

    public class Handler : IRequestHandler<Request, List<DynamicFieldDetailsModel>>
    {
        private readonly DataContext context;

        public Handler(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<DynamicFieldDetailsModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await context.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .SelectMany(p => p.Template.FieldConfigs.Select(f => new DynamicFieldDetailsModel
                {
                    FieldConfigId = f.Id,
                    DisplayName = f.DisplayName,
                    Value = p.DynamicFieldValues.Where(p => p.FieldConfigId == f.Id).Select(p => p.Value).FirstOrDefault()
                }))
                .ToListAsync(cancellationToken);
        }
    }
}