using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Sitemap
{
    public class BreadcrumbItemModel
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public bool Active { get; set; }
    }
}
