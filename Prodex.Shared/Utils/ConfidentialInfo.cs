using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Utils;

public abstract class ConfidentialInfo
{
    public DateTime DateCreatedUtc { get; set; }
    public DateTime DateUpdatedUtc { get; set; }
    public string CreatedByNavigation { get; set; }
    public string UpdatedByNavigation { get; set; }
}
