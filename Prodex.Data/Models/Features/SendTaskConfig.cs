using Prodex.Data.Interfaces;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodex.Data.Models;

public partial class SendTaskConfig : IEntity
{
    [NotMapped]
    public SendTaskTarget TargetTypeEnum
    {
        get => (SendTaskTarget)TargetType;
        set => TargetType = (int)value;
    }
}
