using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
{
    public class CreateHandler : BaseCreateHandler<FormModel, object>
    {
        private readonly DataContext context;

        public CreateHandler(DataContext context)
        {
            this.context = context;
        }

        public override async Task<object> Create(FormModel form, CancellationToken cancellationToken)
        {
            var p = new Process
            {
                Name = form.Name,
                Xml = form.Xml,
            };

            context.Processes.Add(p);

            await context.SaveChangesAsync(cancellationToken);

            return null; // Todo: return detils model
        }
    }
}
