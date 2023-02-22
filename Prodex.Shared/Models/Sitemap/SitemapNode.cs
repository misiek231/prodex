using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Sitemap
{
    public class SitemapNode
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public List<SitemapNode> Children { get; set;}
    }
}
