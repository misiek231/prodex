using MediatR;
using Newtonsoft.Json;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.Handlers.Products;

public class ExecuteServiceTaskHandler : IRequestHandler<ExecuteServiceTaskRequest, object>
{
    private readonly DataContext context;

    public ExecuteServiceTaskHandler(DataContext context)
    {
        this.context = context;
    }

    public async Task<object> Handle(ExecuteServiceTaskRequest request, CancellationToken cancellationToken)
    {
        var conf = context.ServiceTaskConfigs.FirstOrDefault(p => p.StepId == request.ExecutedStep.Id);

        if(conf.ActionTypeEnum == ServiceTaskAction.ChangeStatus)
        {
            var statusModel = JsonConvert.DeserializeObject<ChangeStatusModel>(conf.ConfigJson);
            ((Product)request.Processable).StatusId = statusModel.StatusId;
        }

        context.Update(request.Processable);
        
        await context.SaveChangesAsync(cancellationToken);

        return "";
    }
}
