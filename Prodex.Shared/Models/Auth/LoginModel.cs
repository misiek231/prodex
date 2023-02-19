using FluentValidation;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Auth
{
    public class LoginModel : FormBaseModel<LoginModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override void Rules(ValidationContext validationContext, FluentValidator<LoginModel> model)
        {
            model.RuleFor(p => p.Username).NotEmpty();
            model.RuleFor(p => p.Password).NotEmpty();
        }
    }
}
