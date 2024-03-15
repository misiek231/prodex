using MediatR;
using Prodex.Data;
using Prodex.Shared.Models.Sitemap;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.Sitemap;


public class GetSitemap
{
    public class Request(long userId) : IRequest<SitemapModel>
    {
        public long UserId { get; init; } = userId;
    }

    public class SitemapHandler(DataContext context) : IRequestHandler<Request, SitemapModel>
    {
        private readonly DataContext _context = context;

        public async Task<SitemapModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);

            var model = new SitemapModel
            {
                User = user.Name,
                Nodes =
                [
                    new() {
                        Name = "Strona główna",
                        Route = "",
                        Icon = "fa-chart-line"
                    }
                ]
            };

            if (user.UserTypeEnum == UserType.Admin)
            {
                //model.Nodes.Add(new SitemapNode());
                model.Nodes.Add(new SitemapNode
                {
                    Name = "Administracja:",
                    
                });

                model.Nodes.Add(GetAdministrationNode());
            }

            //model.Nodes.Add(new SitemapNode());
            model.Nodes.Add(new SitemapNode
            {
                Name = "Moduły produktowe:",
            });

            var newNodes = _context.ProductTemplates.ToList();
            foreach (var node in newNodes)
            {
                model.Nodes.Add(new SitemapNode
                {
                    Name = node.Name,
                    Icon = "fa-desktop",
                    Route = $"product/{node.Id}",
                    Children =
                    [
                        new ()
                        {
                            Name = "Rozpocznij nową produkcję",
                            Route = $"add",
                            Hidden = true,
                        },
                        new ()
                        {
                            Name = "Szczegóły produkcji",
                            Route = "details/{Id}",
                            Hidden = true,
                        }
                    ]
                });
            }

            return model;
        }

        private static SitemapNode GetAdministrationNode()
        {
            return new SitemapNode
            {
                Name = "Zarządzanie",
                Route = "administration",
                Icon = "fa-chart-line",
                Children =
                [
                    new() {
                        Name = "Pracownicy",
                        Route = "users",
                        Icon = "fa-user",
                        Children =
                        [
                            new() {
                            Name = "Dodaj Pracownika",
                            Route = "add",
                            Icon = "",
                            Hidden = true
                            }
                        ]
                    },
                    new() {
                        Name = "Słowniki",
                        Route = "dictionary",
                        Icon = "fa-book"
                    },
                    new() {
                        Name = "Szablony produktów",
                        Route = "product-template",
                        Icon = "fa-desktop",
                        Children =
                        [
                            new() {
                                Name = "Formularz",
                                Route = "form",
                                Icon = "",
                                Hidden = true
                            }
                        ]
                    }
                ]
            };
        }
    }
}


