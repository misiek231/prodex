using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Mappers;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data;
using Prodex.Data.Interfaces;
using Prodex.Data.Models;
using Prodex.Shared.Models.Dashboard;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Bussines.Handlers.Dashboard;


public static class DashboardHandler
{

    public class Request(long userId, bool isAdmin) : IRequest<DashboardDetailsModel>
    {
        public long UserId { get; set; } = userId;
        public bool IsAdmin { get; set; } = isAdmin;
    }

    public class Handler(DataContext context, IListMapper<Product, ListItemModel> mapper) : IRequestHandler<Request, DashboardDetailsModel>
    {
        private readonly DataContext _context = context;
        private readonly IListMapper<Product, ListItemModel> _mapper = mapper;

        public async Task<DashboardDetailsModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var templates = await _context.ProductTemplates.ToListAsync(cancellationToken: cancellationToken);

            var model = new DashboardDetailsModel
            {
                Widgets = [],
                IsAdmin = request.IsAdmin
            };

            foreach (var template in templates)
            {
                var pager = new Pager();

                var products = await _context.Products
                    .OrderByDescending(p => p.Id)
                    .Where(p => p.TemplateId == template.Id)
                    .Where(p => (request.IsAdmin || p.ProductTargets.Any(p => p.UserId == request.UserId)) && !p.IsFinished)
                    .Paginate(pager)
                    .ToListItemModel(_mapper)
                    .ToListAsync(cancellationToken: cancellationToken);

                if (products.Count == 0) continue;

                model.Widgets.Add(new WidgetModel
                {
                    Name = template.Name,
                    Products = new Pagination<ListItemModel>(products, pager.TotalRows ?? 0)
                });
            }

            return model;
        }
    }
}
