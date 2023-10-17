using System.Text.Json.Serialization;

namespace Prodex.Shared.Models.Sitemap;

public class SitemapNode
{
    public string Name { get; set; }
    public string Route { get; set; }
    public string Icon { get; set; }
    public bool Hidden { get; set; }
    public List<SitemapNode> Children { get; set; }

    [JsonIgnore]
    public bool HasVisibleChildren => Children?.Any(p => !p.Hidden) == true;

    public void Breadcrumb(List<string> path, List<BreadcrumbItemModel> result)
    {
        path.RemoveAt(0);

        result.Add(new BreadcrumbItemModel
        {
            Name = Name,
            Route = HasVisibleChildren ? null : Route, // TODO: not relay on nullable route, see breadcrumb: administration/users/add
            Active = path.Count == 0,
        });

        if (path.Count == 0) return;

        var node = Children.Single(p => p.Route == FixedPath(p.Route, path));
        var toSkip = node.Route.Count(p => p == '/');
        node.Breadcrumb(path.Skip(toSkip).ToList(), result);
    }

    private string FixedPath(string route, List<string> path)
    {
        if (route.EndsWith("/{Id}"))
        {
            path[^1] = "{Id}";
        }
            
        int v = route.Count(p => p == '/');
        return string.Join("/", path.Take(v + 1));
    }
}
