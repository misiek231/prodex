using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Models.ProductTemplates;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Products;

public class FormModel : FormBaseModel<FormModel>
{
    [Description("Nazwa")]
    public string Name { get; set; }

    [Description("Szablon")]
    public ApiTemplateSelect TemplateId { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
    {
        model.RuleFor(p => p.Name).NotEmpty().LengthCheck(60);
        model.RuleFor(p => p.TemplateId).NotNull();
    }
}
