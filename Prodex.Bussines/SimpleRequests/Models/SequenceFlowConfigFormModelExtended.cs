using Newtonsoft.Json;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel;

namespace Prodex.Bussines.SimpleRequests.Models;

public class SequenceFlowConfigFormModelExtended
{
    public long? TemplateId { get; set; }
    public string FlowId { get; set; }
    public long? LdynamicField { get; set; }
    public long? LDictionaryField { get; set; }
    public string Lvalue { get; set; }
    public OperatorType OperatorType { get; set; }
    public long? RdynamicField { get; set; }
    public long? RDictionaryField { get; set; }
    public string Rvalue { get; set; }

    public SequenceFlowConfigFormModelExtended(SequenceFlowConfigFormModel model)
    {

        switch (model.LOperandType)
        {
            case OperandType.Value:
                Lvalue = model.Lvalue;
                LdynamicField = null;
                LDictionaryField = null;
                break;
        
            case OperandType.DynamicField:
                LdynamicField = model.LdynamicField.Id;
                LDictionaryField = null;
                Lvalue = null;
                break;

            case OperandType.DictionaryTerm:
                LDictionaryField = model.LDictionaryField.Id;
                LdynamicField = null;
                Lvalue = null;
                break;
        }

        OperatorType = model.OperatorType;

        switch (model.ROperandType)
        {
            case OperandType.Value:
                Rvalue = model.Rvalue;
                RdynamicField = null;
                RDictionaryField = null;
                break;
            case OperandType.DynamicField:
                RdynamicField = model.RdynamicField.Id;
                Rvalue = null;
                RDictionaryField = null;
                break;
            case OperandType.DictionaryTerm:
                RDictionaryField = model.RDictionaryField.Id;
                RdynamicField = null;
                Rvalue = null;
                break;
        }
    }

    public SequenceFlowConfigFormModelExtended(SequenceFlowConfigFormModel model, long templateId, string flowId) : this(model)
    {
        TemplateId = templateId;
        FlowId = flowId;
    }
}