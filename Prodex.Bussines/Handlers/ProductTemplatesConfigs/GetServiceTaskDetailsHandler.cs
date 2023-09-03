using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.Handlers.Products;


public static class GetServiceTaskDetails
{

    public class Request : IRequest<ServiceTaskConfigFormModel>
    {
        public long TemplateId { get; set; }
        public string StepId { get; set; }

        public Request(long templateId, string stepId)
        {
            TemplateId = templateId;
            StepId = stepId;
        }
    }

    public class GetServiceTaskDetailsHandler : IRequestHandler<Request, ServiceTaskConfigFormModel>
    {
        private readonly DataContext context;
        private readonly IDetailsMapper<ServiceTaskConfig, ServiceTaskConfigFormModel> mapper;

        public GetServiceTaskDetailsHandler(DataContext context, IDetailsMapper<ServiceTaskConfig, ServiceTaskConfigFormModel> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceTaskConfigFormModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await context.ServiceTaskConfigs
                .FirstOrDefaultAsync(p => p.TemplateId == request.TemplateId && p.StepId == request.StepId, cancellationToken);

            if (result == null) return null;

            return mapper.ToDetailsModel(result);
        }
    }
}