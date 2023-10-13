using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products.DynamicFields;

namespace Prodex.Bussines.Handlers.Products;

public static class SetProductFieldValue
{
    public class Request : IRequest<object>
    {
        public long ProductId { get; set; }
        public long FieldConfigId { get; set; }
        public string NewValue { get; set; }

        public Request(long productId, long fieldConfigId, string newValue)
        {
            ProductId = productId;
            FieldConfigId = fieldConfigId;
            NewValue = newValue;
        }
    }

    public class Handler : IRequestHandler<Request, object>
    {
        private readonly DataContext context;

        public Handler(DataContext context)
        {
            this.context = context;
        }

        public async Task<object> Handle(Request request, CancellationToken cancellationToken)
        {
            var fieldValue = await context.DynamicFieldValues
                .SingleOrDefaultAsync(p => p.FieldConfigId == request.FieldConfigId && p.ProductId == request.ProductId, cancellationToken);

            fieldValue ??= new DynamicFieldValue()
            {
                ProductId = request.ProductId,
                FieldConfigId = request.FieldConfigId
            };

            fieldValue.Value = request.NewValue;

            if(fieldValue.Id == 0) 
                context.Add(fieldValue);
            else
                context.Update(fieldValue);

            await context.SaveChangesAsync(cancellationToken);

            return null;
        }
    }
}