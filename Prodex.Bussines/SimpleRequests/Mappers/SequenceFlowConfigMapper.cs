using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class SequenceFlowConfigMapper :
    ICreateMapper<SequenceFlowConfig, SequenceFlowConfigFormModelExtended>,
    IUpdateMapper<SequenceFlowConfig, SequenceFlowConfigFormModelExtended>
{
    public partial SequenceFlowConfig ToEntity(SequenceFlowConfigFormModelExtended form);

    public void ToEntity(SequenceFlowConfigFormModelExtended form, SequenceFlowConfig entity)
    {
        entity.LdynamicField = form.LdynamicField;
        entity.LDictionaryTermId = form.LDictionaryField;
        entity.Lvalue = form.Lvalue;

        entity.OperatorTypeEnum = form.OperatorType;
        
        entity.RdynamicField = form.RdynamicField;
        entity.RDictionaryTermId = form.RDictionaryField;
        entity.Rvalue = form.Rvalue;
    }
}

