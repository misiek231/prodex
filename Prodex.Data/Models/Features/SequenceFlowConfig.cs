using Prodex.Data.Interfaces;
using Prodex.Shared.Models.ProductTemplates.ElementOptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodex.Data.Models;

public partial class SequenceFlowConfig : IEntity
{
    [NotMapped]
    public OperatorType OperatorTypeEnum
    {
        get => (OperatorType)OperatorType;
        set => OperatorType = (int)value;
    }
}
