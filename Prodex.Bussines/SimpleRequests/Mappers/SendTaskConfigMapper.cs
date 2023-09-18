using Newtonsoft.Json;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.SimpleRequests.Models;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Utils;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class SendTaskConfigMapper :
    ICreateMapper<SendTaskConfig, SendTaskConfigFormModelExtended>,
    IUpdateMapper<SendTaskConfig, SendTaskConfigFormModelExtended>,
    IDetailsMapper<SendTaskConfig, SendTaskConfigFormModel>
{
    public partial SendTaskConfig ToEntity(SendTaskConfigFormModelExtended form);

    public void ToEntity(SendTaskConfigFormModelExtended form, SendTaskConfig entity)
    {
        entity.TargetTypeEnum = form.TargetType;
        entity.UserId = form.UserId;
    }

    public IQueryable<SendTaskConfigFormModel> ToDetailsModel(IQueryable<SendTaskConfig> query)
    {
        throw new NotImplementedException();
    }

    [ObjectFactory]
    private static ApiUserSelect CreateKeyValueResult(User t) => t == null ? null : new(t.Id, t.Name);

    [MapProperty("TargetTypeEnum", "Target")]
    public partial SendTaskConfigFormModel ToDetailsModel(SendTaskConfig model);
}

