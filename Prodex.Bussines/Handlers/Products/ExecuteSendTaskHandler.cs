using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.Handlers.Products;

public class ExecuteSendTaskHandler : IRequestHandler<ExecuteSendTaskRequest, object>
{
    private readonly DataContext context;

    public ExecuteSendTaskHandler(DataContext context)
    {
        this.context = context;
    }

    public async Task<object> Handle(ExecuteSendTaskRequest request, CancellationToken cancellationToken)
    {
        var conf = context.SendTaskConfigs.FirstOrDefault(p => p.StepId == request.ExecutedStep.Id);

        var newTargets = new List<long>();

        if(conf.TargetTypeEnum == SendTaskTarget.User)
        {
            newTargets.Add(conf.UserId);
        }

        context.ProductTargets.Where(p => p.ProductId == request.Processable.Id).ExecuteDelete();

        await context.ProductTargets.AddRangeAsync(newTargets.Select(p => new ProductTarget
        {
            ProductId = request.Processable.Id,
            UserId = p
        }), cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return "";
    }
}
