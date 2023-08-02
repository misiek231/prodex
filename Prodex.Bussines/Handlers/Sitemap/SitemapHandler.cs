using MediatR;
using Prodex.Shared.Models.Sitemap;
using System.Text.Json;

namespace Prodex.Bussines.Sitemap;


public class GetSitemap
{
    public class Request : IRequest<SitemapModel>
    {
        public string SitemapFilePath { get; init; }

        public Request(string sitemapFilePath)
        {
            SitemapFilePath = sitemapFilePath;
        }
    }

    public class SitemapHandler : IRequestHandler<Request, SitemapModel>
    {
        public async Task<SitemapModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var text = await File.ReadAllTextAsync(request.SitemapFilePath, cancellationToken);
            return new SitemapModel { Nodes = JsonSerializer.Deserialize<List<SitemapNode>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) };
        }
    }
}


