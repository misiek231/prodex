using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.Diagnostics;

namespace Prodex.Bussines.Handlers.Products;

public class CheckConditionsHandler : IRequestHandler<CheckConditionsRequest, CheckConditionsResult>
{
    private readonly DataContext context;

    public CheckConditionsHandler(DataContext context)
    {
        this.context = context;
    }

    public async Task<CheckConditionsResult> Handle(CheckConditionsRequest request, CancellationToken cancellationToken)
    {
        var result = new List<string>();

        foreach (var f in request.FlowsToChek)
        {
            var conf = context.SequenceFlowConfigs.FirstOrDefault(p => p.FlowId == f);

            var LValue = (await GetValue(request, conf.LdynamicField, cancellationToken)) ?? conf.Lvalue;
            var RValue = (await GetValue(request, conf.RdynamicField, cancellationToken)) ?? conf.Rvalue;

            var enabled = conf.OperatorTypeEnum switch
            {
                OperatorType.None => throw new InvalidOperationException(),
                OperatorType.Equal => LValue == RValue,
                OperatorType.Lower => throw new NotImplementedException(),
                OperatorType.LowerEqual => throw new NotImplementedException(),
                OperatorType.Bigger => throw new NotImplementedException(),
                OperatorType.BiggerEqual => throw new NotImplementedException(),
                OperatorType.NotEqual => throw new NotImplementedException(),
                _ => throw new UnreachableException()
            };

            if (enabled)
                result.Add(f);
        }

        return new CheckConditionsResult { EnabledFlows = result };
    }

    private async Task<string> GetValue(CheckConditionsRequest request, long? dynamicField, CancellationToken cancellationToken)
    {
        if (!dynamicField.HasValue)
        {
            return null;
        }
        
        return (await context.DynamicFieldValues.Where(p => p.ProductId == request.Processable.Id && p.FieldConfigId == dynamicField).FirstOrDefaultAsync(cancellationToken))?.Value;
    }
}
