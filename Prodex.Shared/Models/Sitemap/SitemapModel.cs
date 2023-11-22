namespace Prodex.Shared.Models.Sitemap
{
    public class SitemapModel
    {
        public List<SitemapNode> Nodes { get; set; }
        public string User { get; set; }
        

        public List<BreadcrumbItemModel> Breadcrumb(string route)
        {
            try
            {
                var result = new List<BreadcrumbItemModel>();
                var path = route.TrimEnd('/').TrimStart('/').Split('/').ToList();
                var node = Nodes.Single(p => p.Route == FixedPath(p.Route, path));

                var toSkip = node.Route.Count(p => p == '/');

                node.Breadcrumb(path.Skip(toSkip).ToList(), result);
                return result;
            }
            catch { return null; }
        }

        private string FixedPath(string route, List<string> path)
        {
            int v = route.Count(p => p == '/');
            return string.Join("/", path.Take(v+1));
        }
    }
}
