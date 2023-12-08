using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Models.Dictionary;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.ProductTemplates.ElementOptions;

public class SequenceFlowConfigFormModel : FormBaseModel<SequenceFlowConfigFormModel>
{
    public long? Id { get; set; }

    [Description("Typ operandu")]
    public OperandType LOperandType { get; set; }

    [Description("Operand")]
    public string Lvalue { get; set; }

    [Description("Operand")]
    public ApiFieldConfig LdynamicField { get; set; }

    [Description("Operand")]
    public ApiDictionaryTermSelect LDictionaryField { get; set; }

    [Description("Operator")]
    public OperatorType OperatorType { get; set; }

    [Description("Typ operandu")]
    public OperandType ROperandType { get; set; }

    [Description("Operand")]
    public string Rvalue { get; set; }

    [Description("Operand")]
    public ApiFieldConfig RdynamicField { get; set; }

    [Description("Operand")]
    public ApiDictionaryTermSelect RDictionaryField { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<SequenceFlowConfigFormModel> model)
    {
        model.RuleFor(p => p.LOperandType).NotEqual(OperandType.None).WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.ROperandType).NotEqual(OperandType.None).WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.OperatorType).NotEqual(OperatorType.None).WithMessage("Pole jest wymagane");
     
        
        model.RuleFor(p => p.Lvalue).NotEmpty().When(p => p.LOperandType == OperandType.Value).WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.Rvalue).NotEmpty().When(p => p.ROperandType == OperandType.Value).WithMessage("Pole jest wymagane");

        model.RuleFor(p => p.LdynamicField).NotNull().When(p => p.LOperandType == OperandType.DynamicField).WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.RdynamicField).NotNull().When(p => p.ROperandType == OperandType.DynamicField).WithMessage("Pole jest wymagane");

        model.RuleFor(p => p.LDictionaryField).NotNull().When(p => p.LOperandType == OperandType.DictionaryTerm).WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.RDictionaryField).NotNull().When(p => p.ROperandType == OperandType.DictionaryTerm).WithMessage("Pole jest wymagane");
    }
}

public enum OperandType
{
    [Description("Brak")]
    None,

    [Description("Wartość")]
    Value,

    [Description("Pole dynamiczne")]
    DynamicField,

    [Description("Wartość słownikowa")]
    DictionaryTerm
}

public enum OperatorType
{
    [Description("Brak")]
    None,

    [Description("<")]
    Lower,

    [Description("<=")]
    LowerEqual,

    [Description(">")]
    Bigger,

    [Description(">=")]
    BiggerEqual,

    [Description("=")]
    Equal,

    [Description("!=")]
    NotEqual,
}

