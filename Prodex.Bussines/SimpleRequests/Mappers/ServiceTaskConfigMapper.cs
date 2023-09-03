using Newtonsoft.Json;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class ServiceTaskConfigMapper :
    ICreateMapper<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>,
    IUpdateMapper<ServiceTaskConfig, ServiceTaskConfigFormModelExtended>,
    IDetailsMapper<ServiceTaskConfig, ServiceTaskConfigFormModel>
{
    public partial ServiceTaskConfig ToEntity(ServiceTaskConfigFormModelExtended form);

    public void ToEntity(ServiceTaskConfigFormModelExtended form, ServiceTaskConfig entity)
    {
        entity.ActionTypeEnum = form.ActionTypeEnum;
        entity.ConfigJson = form.ConfigJson;
    }

    public IQueryable<ServiceTaskConfigFormModel> ToDetailsModel(IQueryable<ServiceTaskConfig> query)
    {
        throw new NotImplementedException();
    }

    public ServiceTaskConfigFormModel ToDetailsModel(ServiceTaskConfig model)
    {
        var result = new ServiceTaskConfigFormModel
        {
            Id = model.Id,
            Action = model.ActionTypeEnum,
        };

        switch (model.ActionTypeEnum)
        {
            case ServiceTaskAction.ChangeStatus:
                result.Status = new(JsonConvert.DeserializeObject<ChangeStatusModel>(model.ConfigJson).StatusId);
                break;
        }

        return result;
    }
}

