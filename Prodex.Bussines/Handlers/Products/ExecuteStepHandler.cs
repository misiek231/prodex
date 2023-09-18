using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;
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
        private readonly IMediator mediator;
        public ExecuteStepHandler(DataContext context, ProcessBuilderService processBuilderService, IMediator mediator)
        {
            this.context = context;
            this.processBuilderService = processBuilderService;
            this.mediator = mediator;
        }

        public async Task<object> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await context.Products
                .Include(p => p.Template)
                .Include(p => p.ProductTargets)
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (!result.ProductTargets.Select(p => p.UserId).Contains(request.UserId))
                throw new UnauthorizedAccessException(); // TODO: return 301 from api

            await processBuilderService
                .BuildProcess(result.Template.ProcessXml)
                .ExecuteUserTask(mediator, result, request.StepId, request.UserId);

            await context.SaveChangesAsync(cancellationToken);

            return Task.FromResult<object>("");
        }
    }
}