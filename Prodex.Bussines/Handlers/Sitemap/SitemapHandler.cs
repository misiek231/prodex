using MediatR;
using Prodex.Data;
using Prodex.Shared.Models.Sitemap;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.Sitemap;


public class GetSitemap
{
    public class Request : IRequest<SitemapModel>
    {
        public long UserId { get; init; }

        public Request(long userId)
        {
            UserId = userId;
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
            var user = await _context.Users.FindAsync(request.UserId);

            var model = new SitemapModel
            {
                User = user.Name,
                Nodes = new List<SitemapNode>
                {
                    new SitemapNode
                    {
                        Name = "Strona główna",
                        Route = "",
                        Icon = "fa-chart-line"
                    }
                }
            };

            if (user.UserTypeEnum == UserType.Admin)
            {
                model.Nodes.Add(GetAdministrationNode());
            }

            var newNodes = _context.ProductTemplates.ToList();
            foreach (var node in newNodes)
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

        private SitemapNode GetAdministrationNode()
        {
            return new SitemapNode
            {
                Name = "Zarządzanie",
                Route = "administration",
                Icon = "fa-chart-line",
                Children = new List<SitemapNode>
                {
                    new SitemapNode
                    {
                        Name = "Pracownicy",
                        Route = "users",
                        Icon = "fa-user",
                        Children = new List<SitemapNode>
                        {
                            new SitemapNode
                            {
                            Name = "Dodaj Pracownika",
                            Route = "add",
                            Icon = "",
                            Hidden = true
                            }
                        }
                    },
                    new SitemapNode
                    {
                        Name = "Słowniki",
                        Route = "dictionary",
                        Icon = "fa-book"
                    },
                    new SitemapNode
                    {
                        Name = "Szablony produktów",
                        Route = "product-template",
                        Icon = "fa-desktop",
                        Children = new List<SitemapNode>
                        {
                            new SitemapNode
                            {
                                Name = "Formularz",
                                Route = "form",
                                Icon = "",
                                Hidden = true
                            }
                        }
                    }
                }
            };
        }
    }
}


