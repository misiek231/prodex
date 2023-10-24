using MediatR;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;

namespace Prodex.Bussines.Handlers.Products;

public class AfterStepExecutedHandler : IRequestHandler<AfterStepExecutedRequest, object>
{
    private readonly DataContext context;

    public AfterStepExecutedHandler(DataContext context)
    {
        this.context = context;
    }

    public async Task<object> Handle(AfterStepExecutedRequest request, CancellationToken cancellationToken)
    {
        context.Histories.Add(new History
        {
            ActionName = request.ExecutedStep.StepType.ToString() + ": " + request.ExecutedStep.Name,
            ProductId = request.Processable.Id,
            UserId = request.UserId,
            DateCreated = DateTime.Now,
            ActionId = request.ExecutedStep.Id
        });

        await context.SaveChangesAsync(cancellationToken);

        return "";
    }
}
