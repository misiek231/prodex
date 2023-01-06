using MediatR;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
{
    public class CreateHandler : IRequestHandler<FormModel>
    {
        private readonly DataContext context;

        public CreateHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(FormModel model, CancellationToken cancellationToken)
        {
            var p = new Process
            {
                Name = model.Name,
                Xml = model.Xml,
            };

            context.Processes.Add(p);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
