using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;
using Prodex.Shared.Models.Products;
using static Prodex.Bussines.SimpleRequests.Base.SimpleGetDetails;

namespace Prodex.Bussines.Handlers.Products
{
    public class GetProductDetailsHandler : IRequestHandler<Request<Product, DetailsModel>, DetailsModel>
    {
        private readonly ProcessBuilderService processBuilderService;
        private readonly DataContext context;

        private readonly IDetailsMapper<Product, DetailsModel> Mapper;

        public GetProductDetailsHandler(ProcessBuilderService processBuilderService, IDetailsMapper<Product, DetailsModel> mapper, DataContext context)
        {
            this.processBuilderService = processBuilderService;
            Mapper = mapper;
            this.context = context;
        }

        public async Task<DetailsModel> Handle(Request<Product, DetailsModel> request, CancellationToken cancellationToken)
        {
            var result = await context.Products
                .Include(p => p.Template)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            var model = Mapper.ToDetailsModel(result);

            var actions = processBuilderService.GetActions(result.Template.ProcessXml, result.CurrentStepId);

            model.Buttons = actions.Select(p => new ApiButton { ActionId = p.Key, Name = p.Value }).ToList();

            return model;
        }
    }
}
