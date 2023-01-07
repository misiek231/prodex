using FluentValidation;
using MediatR;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.Processes
{
    public class FormModel : FormBaseModel, IValidatable<FormModel>, IRequest
    {
        public string Name { get; set; }
        public string Xml { get; set; }

        public void Rules(ValidationContext validationContext, FluentValidator<FormModel> model)
        {
            model.RuleFor(p => p.Name).LengthCheck(60);
        }
    }
}
