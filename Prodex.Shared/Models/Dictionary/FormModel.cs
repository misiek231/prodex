using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Dictionary;

public class FormModel : FormBaseModel<FormModel>
{
    [Description("Nazwa")]
    public string Name { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
    {
        model.RuleFor(p => p.Name).NotEmpty().LengthCheck(60);
    }
}
