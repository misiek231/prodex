using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Data.Interfaces
{
    public interface IConfidential
    {
        DateTime DateCreatedUtc { get; set; }
        DateTime DateUpdatedUtc { get; set; }
        long? CreatedBy { get; set; }
        long? UpdatedBy { get; set; }
    }
}
