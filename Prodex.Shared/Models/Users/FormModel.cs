using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Users;

public class FormModel : FormBaseModel<FormModel>
{
    [Description("Imię")]
    public string GivenName { get; set; }
    [Description("Nazwisko")]
    public string Surname { get; set; }
    [Description("Nazwa użytkownika")]
    public string Username { get; set; }
    [Description("Hasło")]
    public string Password { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
    {
        model.RuleFor(p => p.GivenName).NotEmpty().LengthCheck(60);
        model.RuleFor(p => p.Surname).NotEmpty().LengthCheck(60);
        model.RuleFor(p => p.Username).NotEmpty().LengthCheck(60);
        model.RuleFor(p => p.Password).NotEmpty().LengthCheck(60);
    }
}
