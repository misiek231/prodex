using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Validation
{
    public class FluentValidator<T> : AbstractValidator<T>
    {

    }

    public interface IValidatable<T> where T : class
    {
        public void Rules(ValidationContext validationContext, FluentValidator<T> model);
    }
}
