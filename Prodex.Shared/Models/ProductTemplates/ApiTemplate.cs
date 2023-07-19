using Prodex.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.ProductTemplates
{
    public class ApiTemplateSelect : IApiSelect
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
}
