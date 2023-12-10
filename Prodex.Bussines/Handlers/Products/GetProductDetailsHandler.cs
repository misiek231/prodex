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
        private readonly IMediator mediator;

        private readonly IDetailsMapper<Product, DetailsModel> Mapper;

        public GetProductDetailsHandler(ProcessBuilderService processBuilderService, IDetailsMapper<Product, DetailsModel> mapper, DataContext context, IMediator mediator)
        {
            this.processBuilderService = processBuilderService;
            Mapper = mapper;
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<DetailsModel> Handle(Request<Product, DetailsModel> request, CancellationToken cancellationToken)
        {
            var result = await context.Products
                .Include(p => p.Template)
                .Include(p => p.Status)
                .Include(p => p.ProductTargets).ThenInclude(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            var model = Mapper.ToDetailsModel(result);

            model.RealizingUser = result.IsFinished ? "-" : string.Join(", ", result.ProductTargets?.Select(p => $"{p.User?.Name} ({p.User.Username})") ?? Array.Empty<string>());

            // Przyciski są dostępne tylko dla targetów
            if (result.ProductTargets.Select(p => p.UserId).Contains(request.UserId))
            {
                var actions = await processBuilderService
                    .BuildProcess(result.Template.ProcessXml)
                    .GetActions(mediator, result);

                model.Buttons = actions.Select(p => new ApiButton { ActionId = p.Key, Name = p.Value }).ToList();
            }
            else
            {
                model.Buttons = new List<ApiButton>();
            }


            return model;
        }
    }
}
