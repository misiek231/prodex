using FluentValidation.Results;

namespace Prodex.Shared.Forms;

public class ValidationErrors
{
    public ValidationErrors() { }
    public ValidationErrors(List<ValidationFailure> errors)
    {
        Errors = errors.Select(p => new ValidationError
        {
            Name = p.PropertyName,
            Message = p.ErrorMessage
        });
    }

    public ValidationErrors(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; set; }
    public bool HasErrors => Errors != null && Errors.Any();
}

public class ValidationError
{
    public string Name { get; set; }
    public string Message { get; set; }
}
