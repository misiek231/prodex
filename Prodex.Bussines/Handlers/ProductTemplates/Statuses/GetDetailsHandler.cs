using AutoMapper;
using MediatR;
using Prodex.Bussines.Requests.ProductTemplates.Statuses;
using Prodex.Data;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.Handlers.ProductTemplates.Statuses
{
    public class GetDetailsHandler : IRequestHandler<GetDetails, FormModel>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public GetDetailsHandler(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FormModel> Handle(GetDetails request, CancellationToken cancellationToken)
        {
            var result = await context.PtStatuses.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            return mapper.Map<FormModel>(result);
        }
    }
}
