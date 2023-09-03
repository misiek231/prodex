using MediatR;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;

namespace Prodex.Bussines.Handlers.Products;

public static class RunProcess
{
    public class Command : SimpleCreate.IAfterCreateRequest<Product>
    {
        public Product Enity { get; set; }
    }

    public class RunProcessHandler : IRequestHandler<Command, object>
    {
        private readonly DataContext context;
        private readonly ProcessBuilderService processBuilder;
        private readonly IMediator mediator;

        public RunProcessHandler(DataContext context, ProcessBuilderService processBuilder, IMediator mediator)
        {
            this.context = context;
            this.processBuilder = processBuilder;
            this.mediator = mediator;
        }

        public async Task<object> Handle(Command request, CancellationToken cancellationToken)
        {

            var template = context.ProductTemplates.Find(request.Enity.TemplateId);

            await processBuilder
                .BuildProcess(template.ProcessXml)
                .Run(mediator, request.Enity, 1); // TODO: pass injected user Id

            context.Update(request.Enity);

            await context.SaveChangesAsync(cancellationToken);

            return "";
        }
    }
}


