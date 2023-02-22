using MediatR;
using Prodex.Bussines.Requests;
using Prodex.Shared.Models.Sitemap;
using System.Text.Json;

namespace Prodex.Bussines.Sitemap;

public class SitemapHandler : IRequestHandler<GetSitemapRequest, SitemapModel>
{
    public async Task<SitemapModel> Handle(GetSitemapRequest request, CancellationToken cancellationToken)
    {
        var text = await File.ReadAllTextAsync(request.SitemapFilePath, cancellationToken);
        return new SitemapModel { Nodes = JsonSerializer.Deserialize<List<SitemapNode>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) };
    }
}
