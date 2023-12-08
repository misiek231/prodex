using Prodex.Data.Interfaces;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodex.Data.Models;

public partial class SequenceFlowConfig : IEntity
{
    [NotMapped]
    public OperatorType OperatorTypeEnum
    {
        get => (OperatorType)OperatorType;
        set => OperatorType = (int)value;
    }

    public OperandType GetOperandType(OperandSide side)
    {
        if (!string.IsNullOrEmpty(GetValue(side))) return OperandType.Value;
        if (GetDynamicFieldId(side).HasValue) return OperandType.DynamicField;
        if (GetDictionaryTermId(side).HasValue) return OperandType.DictionaryTerm;

        return OperandType.None;
    }

    public string GetValue(OperandSide side)
    {
        if(side == OperandSide.Left) return Lvalue;

        return Rvalue;
    }

    public long? GetDictionaryTermId(OperandSide side)
    {
        if (side == OperandSide.Left) return LDictionaryTermId;

        return RDictionaryTermId;
    }

    public DictionaryTerm GetDictionaryTerm(OperandSide side)
    {
        if (side == OperandSide.Left) return LDictionaryTerm;

        return RDictionaryTerm;
    }

    public long? GetDynamicFieldId(OperandSide side)
    {
        if (side == OperandSide.Left) return LdynamicField;

        return RdynamicField;
    }

    public FieldConfig GetDynamicField(OperandSide side)
    {
        if (side == OperandSide.Left) return LdynamicFieldNavigation;

        return RdynamicFieldNavigation;
    }
}

public enum OperandSide
{
    Left,
    Right
}
