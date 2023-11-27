using Prodex.Data.Interfaces;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodex.Data.Models;

public partial class ServiceTaskConfig : IEntity, IConfidential
{
    [NotMapped]
    public ServiceTaskAction ActionTypeEnum
    {
        get => (ServiceTaskAction)ActionType;
        set => ActionType = (int)value;
    }
}
