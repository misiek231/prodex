using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Products.DynamicFields;

public class SetProductFieldFormModel : FormBaseModel<SetProductFieldFormModel>
{
    public string NewValue {  get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<SetProductFieldFormModel> model)
    {
        model.RuleFor(p => p.NewValue).LengthCheck(5000);
    }
}
