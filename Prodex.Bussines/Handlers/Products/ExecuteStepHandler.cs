using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;

namespace Prodex.Bussines.Handlers.Products;

public class ExecuteStep
{
    public class Request : IRequest<object>
    {
        public long ProductId { get; set; }
        public long StepId { get; set; }
        public long UserId { get; set; }

        public Request(long productId, long stepId, long userId)
        {
            ProductId = productId;
            StepId = stepId;
            UserId = userId;
        }
    }

    public class ExecuteStepHandler : IRequestHandler<Request, object>
    {
        private readonly DataContext context;
        private readonly ProcessBuilderService processBuilderService;
        public ExecuteStepHandler(DataContext context, ProcessBuilderService processBuilderService)
        {
            this.context = context;
            this.processBuilderService = processBuilderService;
        }

        public async Task<object> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await context.Products.Include(p => p.Template).FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            var actions = processBuilderService.GetActions(result.Template.ProcessXml, result.CurrentStepId);

            var action = actions.FirstOrDefault(p => p.Key == request.StepId);

            if (action is null)
                throw new InvalidOperationException();

            context.Histories.Add(new History
            {
                ActionName = action.Value,
                ProductId = request.ProductId,
                UserId = request.UserId,
                DateCreated = DateTime.Now,
            });

            result.CurrentStepId = request.StepId;

            await context.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>("");
        }
    }
}