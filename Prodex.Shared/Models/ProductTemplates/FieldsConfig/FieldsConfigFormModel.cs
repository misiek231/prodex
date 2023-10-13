using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.ProductTemplates.FieldsConfig;

public class FieldsConfigFormModel : FormBaseModel<FieldsConfigFormModel>
{
    public List<FieldModel> Fields { get; set; } = new();

    public override void Rules(ValidationContext validationContext, FluentValidator<FieldsConfigFormModel> model)
    {
        // model.RuleFor(p => p.Fields).NotNull();
    }
}
