using Prodex.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Products
{
    public class DetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public KeyValueResult Template { get; set; }
        public List<ApiButton> Buttons { get; set; }
    }
}
