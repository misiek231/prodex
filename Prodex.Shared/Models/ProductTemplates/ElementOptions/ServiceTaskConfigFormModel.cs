using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.ProductTemplates.ElementOptions;

public class ServiceTaskConfigFormModel : FormBaseModel<ServiceTaskConfigFormModel>
{
    public long? Id { get; set; }

    [Description("Akcja")]
    public ServiceTaskAction Action { get; set; }

    [Description("Status")]
    public ApiStatusSelect Status { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<ServiceTaskConfigFormModel> model)
    {
    }
}

public enum ServiceTaskAction
{
    [Description("Brak")]
    None,

    [Description("Zmień status")]
    ChangeStatus
}
