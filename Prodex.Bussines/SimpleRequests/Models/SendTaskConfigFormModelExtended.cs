using Newtonsoft.Json;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.SimpleRequests.Models;

public class SendTaskConfigFormModelExtended
{
    public SendTaskTarget TargetType { get; set; }
    public long? TemplateId { get; set; }
    public string StepId { get; set; }
    public long UserId { get; set; }

    public SendTaskConfigFormModelExtended(SendTaskConfigFormModel model)
    {
        TargetType = model.Target;
        UserId = model.User.Id;
    }

    public SendTaskConfigFormModelExtended(SendTaskConfigFormModel model, long templateId, string stepId) : this(model)
    {
        TemplateId = templateId;
        StepId = stepId;
    }
}
