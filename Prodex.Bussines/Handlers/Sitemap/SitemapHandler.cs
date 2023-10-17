using MediatR;
using Prodex.Data;
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
        private readonly DataContext _context;

        public SitemapHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<SitemapModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var text = await File.ReadAllTextAsync(request.SitemapFilePath, cancellationToken);
            var model = new SitemapModel { Nodes = JsonSerializer.Deserialize<List<SitemapNode>>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) };

            var newNodes = _context.ProductTemplates.ToList();


            foreach( var node in newNodes )
            {
                model.Nodes.Add(new SitemapNode
                {
                    Name = node.Name,
                    Icon = "fa-desktop",
                    Route = $"product/{node.Id}",
                    Children = new List<SitemapNode>
                    {
                        new SitemapNode
                        {
                            Name = "Rozpocznij nową produkcję",
                            Route = $"add",
                            Hidden = true,
                        },
                        new SitemapNode
                        {
                            Name = "Szczegóły produkcji",
                            Route = "details/{Id}",
                            Hidden = true,
                        }
                    }
                });
            }

            return model;
        }
    }
}


