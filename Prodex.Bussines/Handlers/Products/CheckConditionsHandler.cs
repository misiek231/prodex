using Blazorise;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Processes;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;
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
            var conf = context.SequenceFlowConfigs
                .Include(p => p.LdynamicFieldNavigation)
                .Include(p => p.RdynamicFieldNavigation)
                .Include(p => p.LDictionaryTerm)
                .Include(p => p.RDictionaryTerm)
                .FirstOrDefault(p => p.FlowId == f);

            (ITypedValue l, ITypedValue r) = await GetTypedValues(request, conf);

            if (l.Compare(conf.OperatorTypeEnum, r))
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

    private async Task<(ITypedValue l, ITypedValue r)> GetTypedValues(CheckConditionsRequest request, SequenceFlowConfig conf)
    {
        return (await GetTypedValue(request, conf, OperandSide.Left), await GetTypedValue(request, conf, OperandSide.Right));
    }

    private async Task<ITypedValue> GetTypedValue(CheckConditionsRequest request, SequenceFlowConfig conf, OperandSide side)
    {
        var oprandType = conf.GetOperandType(side);

        if (oprandType == OperandType.Value) return new TypedValueText(conf.GetValue(side));

        if (oprandType == OperandType.DynamicField)
        {
            var fieldValue = (await context.DynamicFieldValues.Where(p => p.ProductId == request.Processable.Id && p.FieldConfigId == conf.GetDynamicFieldId(side))
                .FirstOrDefaultAsync())?.Value;

            if (conf.GetDynamicField(side).FieldTypeEnum == FieldType.Text)
            {
                return new TypedValueText(fieldValue);
            }

            if (conf.GetDynamicField(side).FieldTypeEnum == FieldType.Number)
            {
                return new TypedValueNumber(int.Parse(fieldValue));
            }

            if (conf.GetDynamicField(side).FieldTypeEnum == FieldType.Dictionary)
            {
                return new TypedValueDict(long.Parse(fieldValue));
            }
        }

        if (oprandType == OperandType.DictionaryTerm) return new TypedValueDict(conf.GetDictionaryTermId(side).Value);

        throw new InvalidOperationException();
    }

    interface ITypedValue
    {
        public bool Compare(OperatorType op, ITypedValue typedValue);
    }

    class TypedValueText(string value) : ITypedValue
    {
        public string Value => value;

        public bool Compare(OperatorType op, ITypedValue right)
        {
            if (right is TypedValueDict) return false;

            if(right is TypedValueNumber r)
            {
                if(int.TryParse(value, out int l))
                {
                    return op switch
                    {
                        OperatorType.None => throw new InvalidOperationException(),
                        OperatorType.Equal => l == r.Value,
                        OperatorType.Lower => l < r.Value,
                        OperatorType.LowerEqual => l <= r.Value,
                        OperatorType.Bigger => l > r.Value,
                        OperatorType.BiggerEqual => l >= r.Value,
                        OperatorType.NotEqual => l != r.Value,
                        _ => throw new UnreachableException()
                    };
                }
                else
                {
                    return op switch
                    {
                        OperatorType.None => throw new InvalidOperationException(),
                        OperatorType.Equal => string.Compare(value, r.Value.ToString()) == 0,
                        OperatorType.Lower => string.Compare(value, r.Value.ToString()) < 0,
                        OperatorType.LowerEqual => string.Compare(value, r.Value.ToString()) <= 0,
                        OperatorType.Bigger => string.Compare(value, r.Value.ToString()) > 0,
                        OperatorType.BiggerEqual => string.Compare(value, r.Value.ToString()) >= 0,
                        OperatorType.NotEqual => string.Compare(value, r.Value.ToString()) != 0,
                        _ => throw new UnreachableException()
                    };
                }
            };

            if(right is TypedValueText r2)
            {
                return op switch
                {
                    OperatorType.None => throw new InvalidOperationException(),
                    OperatorType.Equal => string.Compare(value, r2.Value.ToString()) == 0,
                    OperatorType.Lower => string.Compare(value, r2.Value.ToString()) < 0,
                    OperatorType.LowerEqual => string.Compare(value, r2.Value.ToString()) <= 0,
                    OperatorType.Bigger => string.Compare(value, r2.Value.ToString()) > 0,
                    OperatorType.BiggerEqual => string.Compare(value, r2.Value.ToString()) >= 0,
                    OperatorType.NotEqual => string.Compare(value, r2.Value.ToString()) != 0,
                    _ => throw new UnreachableException()
                };
            }

            return false;
        }
    }

    class TypedValueNumber(int value) : ITypedValue
    {
        public int Value => value;

        public bool Compare(OperatorType op, ITypedValue right)
        {
            if (right is TypedValueDict) return false;

            if (right is TypedValueText ri)
            {
                if (int.TryParse(ri.Value, out int r))
                {
                    return op switch
                    {
                        OperatorType.None => throw new InvalidOperationException(),
                        OperatorType.Equal => value == r,
                        OperatorType.Lower => value < r,
                        OperatorType.LowerEqual => value <= r,
                        OperatorType.Bigger => value > r,
                        OperatorType.BiggerEqual => value >= r,
                        OperatorType.NotEqual => value != r,
                        _ => throw new UnreachableException()
                    };
                }
                else
                {
                    return op switch
                    {
                        OperatorType.None => throw new InvalidOperationException(),
                        OperatorType.Equal => string.Compare(value.ToString(), ri.Value.ToString()) == 0,
                        OperatorType.Lower => string.Compare(value.ToString(), ri.Value.ToString()) < 0,
                        OperatorType.LowerEqual => string.Compare(value.ToString(), ri.Value.ToString()) <= 0,
                        OperatorType.Bigger => string.Compare(value.ToString(), ri.Value.ToString()) > 0,
                        OperatorType.BiggerEqual => string.Compare(value.ToString(), ri.Value.ToString()) >= 0,
                        OperatorType.NotEqual => string.Compare(value.ToString(), ri.Value.ToString()) != 0,
                        _ => throw new UnreachableException()
                    };
                }
            };

            if (right is TypedValueNumber r2)
            {
                return op switch
                {
                    OperatorType.None => throw new InvalidOperationException(),
                    OperatorType.Equal => value == r2.Value,
                    OperatorType.Lower => value < r2.Value,
                    OperatorType.LowerEqual => value <= r2.Value,
                    OperatorType.Bigger => value > r2.Value,
                    OperatorType.BiggerEqual => value >= r2.Value,
                    OperatorType.NotEqual => value != r2.Value,
                    _ => throw new UnreachableException()
                };
            }

            return false;
        }
    }

    class TypedValueDict(long value) : ITypedValue
    {
        public long Value => value;
        public bool Compare(OperatorType op, ITypedValue right)
        {
            if (right is not TypedValueDict d) return false;

            return op switch
            {
                OperatorType.None => throw new InvalidOperationException(),
                OperatorType.Equal => value == d.Value,
                OperatorType.Lower => throw new InvalidOperationException(),
                OperatorType.LowerEqual => throw new InvalidOperationException(),
                OperatorType.Bigger => throw new InvalidOperationException(),
                OperatorType.BiggerEqual => throw new InvalidOperationException(),
                OperatorType.NotEqual => value != d.Value,
                _ => throw new UnreachableException()
            };
        }
    }
}
