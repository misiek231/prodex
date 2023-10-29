using Newtonsoft.Json;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel;

namespace Prodex.Bussines.SimpleRequests.Models;

public class SequenceFlowConfigFormModelExtended
{
    public long? TemplateId { get; set; }
    public string FlowId { get; set; }
    public long? LdynamicField { get; set; }
    public string Lvalue { get; set; }
    public OperatorType OperatorType { get; set; }
    public long? RdynamicField { get; set; }
    public string Rvalue { get; set; }

    public SequenceFlowConfigFormModelExtended(SequenceFlowConfigFormModel model)
    {
        if(model.LOperandType == OperandType.Value)
        {
            LdynamicField = null;
            Lvalue = model.Lvalue;
        }
        else
        {
            LdynamicField = model.LdynamicField.Id;
            Lvalue = null;
        }

        OperatorType = model.OperatorType;

        if (model.ROperandType == OperandType.Value)
        {
            RdynamicField = null;
            Rvalue = model.Rvalue;
        }
        else
        {
            RdynamicField = model.RdynamicField.Id;
            Rvalue = null;
        }
    }

    public SequenceFlowConfigFormModelExtended(SequenceFlowConfigFormModel model, long templateId, string flowId) : this(model)
    {
        TemplateId = templateId;
        FlowId = flowId;
    }
}