using Blazorise;
using Prodex.Shared.Models.Processes;
using Prodex.Shared.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Prodex.Shared.Forms
{

    public abstract class FormBaseModel
    {
        public abstract ValidationErrors Validate(ValidationContext validationContext);
    }

    public abstract class FormBaseModel<T> : FormBaseModel, IValidatable<T> where T : class
    {
        private ValidationErrors _errors;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<ValidationError> Errors => _errors?.Errors;
        [JsonIgnore]
        public bool HasErrors => _errors != null && _errors.HasErrors;

        [JsonIgnore]
        public Validations Validations;

        public abstract void Rules(ValidationContext validationContext, FluentValidator<T> model);

        public void WithErrors(string errors)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _errors = JsonSerializer.Deserialize<ValidationErrors>(errors, options);
        }

        public void Status(ValidatorEventArgs a, string propName)
        {
            a.Status = _errors?.Errors.Any(p => p.Name.Equals(propName, StringComparison.OrdinalIgnoreCase)) == true ? ValidationStatus.Error : ValidationStatus.Success;
        }
        
        public string Message(string propName)
        {
            return _errors?.Errors.Single(p => p.Name.Equals(propName, StringComparison.OrdinalIgnoreCase)).Message;
        }

        public void ClearErrors()
        {
            _errors = null;
        }

        public override sealed ValidationErrors Validate(ValidationContext validationContext)
        {
            ClearErrors();
            var validator = new FluentValidator<T>();
            Rules(validationContext, validator);
            _errors = new ValidationErrors(validator.Validate(this as T).Errors);

            Validations?.ValidateAll();
            return _errors;
        }
    }
}
