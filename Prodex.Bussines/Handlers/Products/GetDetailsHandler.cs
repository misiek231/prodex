using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Requests.Products;
using Prodex.Data;
using Prodex.Processes;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.Handlers.Products
{
    public class GetDetailsHandler : IRequestHandler<GetDetails, DetailsModel>
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ProcessBuilderService processBuilderService;
        public GetDetailsHandler(DataContext context, IMapper mapper, ProcessBuilderService processBuilderService)
        {
            this.context = context;
            this.mapper = mapper;
            this.processBuilderService = processBuilderService;
        }

        public async Task<DetailsModel> Handle(GetDetails request, CancellationToken cancellationToken)
        {
            var result = await context.Products.Include(p => p.Template).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            var model = mapper.Map<DetailsModel>(result);
            
            var actions = processBuilderService.GetActions(result.Template.ProcessXml, result.CurrentStepId);

            model.Buttons = actions.Select(p => new ApiButton { ActionId = p.id, Name = p.name }).ToList(); // new List<ApiButton> { new ApiButton { ActionId = 1, Name = "Test" } };

            return model;
        }
    }
}
