using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Auth;

public class LoginModel : FormBaseModel<LoginModel>
{
    [Description("Nazwa użytkownika")]
    public string Username { get; set; }
    [Description("Hasło")]
    public string Password { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<LoginModel> model)
    {
        model.RuleFor(p => p.Username).NotEmpty().WithMessage("Pole jest wymagane");
        model.RuleFor(p => p.Password).NotEmpty().WithMessage("Pole jest wymagane");
    }
}
