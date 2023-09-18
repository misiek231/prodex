using Prodex.Shared.Forms;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prodex.Shared.Models.ProductTemplates.ElementOptions;

public class SendTaskConfigFormModel : FormBaseModel<SendTaskConfigFormModel>
{
    public long? Id { get; set; }

    [Description("Wyślij do")]
    public SendTaskTarget Target { get; set; }

    [Description("Użytkownik")]
    public ApiUserSelect User { get; set; }


    public override void Rules(ValidationContext validationContext, FluentValidator<SendTaskConfigFormModel> model)
    {
    }
}

public enum SendTaskTarget
{
    [Description("Brak")]
    None,

    [Description("Użytkownik")]
    User
}
