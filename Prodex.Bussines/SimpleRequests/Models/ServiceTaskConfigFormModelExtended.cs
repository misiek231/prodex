using Newtonsoft.Json;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.SimpleRequests.Models;

public class ServiceTaskConfigFormModelExtended
{
    public ServiceTaskAction ActionTypeEnum { get; set; }
    public long? TemplateId { get; set; }
    public string StepId { get; set; }
    public object ConfigObject { get; set; }

    public ServiceTaskConfigFormModelExtended(ServiceTaskConfigFormModel model)
    {
        ActionTypeEnum = model.Action;
        ConfigObject = GetConfigModel(model);
    }

    public ServiceTaskConfigFormModelExtended(ServiceTaskConfigFormModel model, long templateId, string stepId)
    {
        ActionTypeEnum = model.Action;
        TemplateId = templateId;
        StepId = stepId;
        ConfigObject = GetConfigModel(model);
    }

    private object GetConfigModel(ServiceTaskConfigFormModel model) => model.Action switch
    {
        ServiceTaskAction.ChangeStatus => new ChangeStatusModel(model),
        _ => throw new InvalidOperationException()
    };
}

public class ChangeStatusModel
{
    public long StatusId { get; set; }

    public ChangeStatusModel() { }

    public ChangeStatusModel(ServiceTaskConfigFormModel model)
    {
        StatusId = model.Status.Id;
    }
}
