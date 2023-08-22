using Prodex.Shared.Forms;
using Prodex.Shared.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.ProductTemplates.ElementOptions;

public class ServiceTaskOptionsModel : FormBaseModel<ServiceTaskOptionsModel>
{
    [Description("Akcja")]
    public UserTaskAction Action { get; set; }

    [Description("Status")]
    public ApiStatusSelect Status { get; set; }

    public override void Rules(ValidationContext validationContext, FluentValidator<ServiceTaskOptionsModel> model)
    {
    }
}

public enum UserTaskAction
{
    [Description("Brak")]
    None,

    [Description("Zmień status")]
    ChangeStatus
}
