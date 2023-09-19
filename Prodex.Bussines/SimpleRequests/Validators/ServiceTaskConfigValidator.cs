using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data;
using Prodex.Data.Models;
using Prodex.Shared.Forms;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;

namespace Prodex.Bussines.SimpleRequests.Validators;

public class ServiceTaskConfigValidator : IValidator<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>
{
    private readonly DataContext _dataContext;

    public ServiceTaskConfigValidator(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public ValidationErrors ValidateCreate(ServiceTaskConfigFormModelExtended model)
    {
        return Validate(model, model.TemplateId.Value);
    }

    public ValidationErrors ValidateUpdate(ServiceTaskConfig entity, ServiceTaskConfigFormModelExtended model)
    {
        return Validate(model, entity.TemplateId);
    }

    private ValidationErrors Validate(ServiceTaskConfigFormModelExtended model, long templateId)
    {
        var result = new List<ValidationError>();

        switch (model.ActionTypeEnum)
        {
            case ServiceTaskAction.ChangeStatus:

                var config = model.ConfigObject as ChangeStatusModel;

                if (!_dataContext.PtStatuses.Where(p => p.TemplateId == templateId && p.Id == config.StatusId).Any())
                    result.Add(new ValidationError { Name = nameof(ServiceTaskConfigFormModel.Status), Message = "Status jest nieprawidłowy" });
                break;

            default:
                throw new InvalidOperationException();
        }

        return new ValidationErrors(result);
    }

}
