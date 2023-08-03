using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Models.Dictionary;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.DictionaryTerms;

public class FormModel : FormBaseModel<FormModel>
{
    [Description("Wartość")]
    public string Value { get; set; }

    [Description("Kategoria słownika")]
    public ApiDictionarySelect DictionaryId { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
    {
        model.RuleFor(p => p.Value).NotEmpty().LengthCheck(60);
        model.RuleFor(p => p.DictionaryId).NotNull();
    }
}
