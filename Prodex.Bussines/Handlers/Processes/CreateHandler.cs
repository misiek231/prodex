using AutoMapper;
using Prodex.Bussines.HandlersHelpers;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.Processes;

namespace Prodex.Bussines.Handlers.Processes
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
            context.Processes.Add(mapper.Map<Process>(form));
            await context.SaveChangesAsync(cancellationToken);
            return null; // Todo: return detils model
        }

        public override Task<object> Update(long id, FormModel form, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
