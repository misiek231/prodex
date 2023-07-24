using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Requests.Products;
using Prodex.Data;
using Prodex.Processes;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.Handlers.Products
{
    public class ExecuteStepHandler : IRequestHandler<ExecuteStepRequest, object>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ProcessBuilderService processBuilderService;
        public ExecuteStepHandler(DataContext context, IMapper mapper, ProcessBuilderService processBuilderService)
        {
            this.context = context;
            this.mapper = mapper;
            this.processBuilderService = processBuilderService;
        }

        public async Task<object> Handle(ExecuteStepRequest request, CancellationToken cancellationToken)
        {
            var result = await context.Products.Include(p => p.Template).FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            var actions = processBuilderService.GetActions(result.Template.ProcessXml, result.CurrentStepId);

            if (!actions.Any(p => p.id == request.StepId))
                throw new InvalidOperationException();

            result.CurrentStepId = request.StepId;

            context.SaveChanges();

            return Task.FromResult<object>("");
        }
    }
}
