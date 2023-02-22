using MediatR;
using Prodex.Shared.Models.Sitemap;

namespace Prodex.Bussines.Requests
{
    public class GetSitemapRequest : IRequest<SitemapModel>
    {
        public string SitemapFilePath { get; init; }

        public GetSitemapRequest(string sitemapFilePath)
        {
            SitemapFilePath = sitemapFilePath;
        }
    }
}
