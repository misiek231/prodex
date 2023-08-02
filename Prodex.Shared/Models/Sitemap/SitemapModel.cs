namespace Prodex.Shared.Models.Sitemap
{
    public class SitemapModel
    {
        public List<SitemapNode> Nodes { get; set; }


        public List<BreadcrumbItemModel> Breadcrumb(string route)
        {
            try
            {
                var result = new List<BreadcrumbItemModel>();
                var path = route.TrimEnd('/').TrimStart('/').Split('/').ToList();
                Nodes.Single(p => p.Route == path[0]).Breadcrumb(path, result);
                return result;
            }
            catch { return null; }
        }
    }
}
