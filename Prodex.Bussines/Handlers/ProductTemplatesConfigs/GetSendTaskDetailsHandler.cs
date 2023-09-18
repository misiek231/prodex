using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.Handlers.ProductTemplatesConfigs;


public static class GetSendTaskDetails
{

    public class Request : IRequest<SendTaskConfigFormModel>
    {
        public long TemplateId { get; set; }
        public string StepId { get; set; }

        public Request(long templateId, string stepId)
        {
            TemplateId = templateId;
            StepId = stepId;
        }
    }

    public class GetSendTaskDetailsHandler : IRequestHandler<Request, SendTaskConfigFormModel>
    {
        private readonly DataContext context;
        private readonly IDetailsMapper<SendTaskConfig, SendTaskConfigFormModel> mapper;

        public GetSendTaskDetailsHandler(DataContext context, IDetailsMapper<SendTaskConfig, SendTaskConfigFormModel> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SendTaskConfigFormModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await context.SendTaskConfigs
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.TemplateId == request.TemplateId && p.StepId == request.StepId, cancellationToken);

            if (result == null) return null;

            return mapper.ToDetailsModel(result);
        }
    }
}