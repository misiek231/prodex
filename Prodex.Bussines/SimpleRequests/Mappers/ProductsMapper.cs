using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Utils;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class ProductsMapper : IListMapper<Product, ListItemModel>, ICreateMapper<Product, FormModel>, IUpdateMapper<Product, FormModel>, IDetailsMapper<Product, DetailsModel>
{
    [ObjectFactory]
    private static TemplateDetailsModel CreateTemplateDetailsModel(ProductTemplate t) => new(t.Id, t.Name, t.ProcessXml);

    [ObjectFactory]
    private static KeyValueResult CreateKeyValueResult(ProductTemplate t) => new(t.Id, t.Name);

    [ObjectFactory]
    private static ApiStatus CreateKeyValueResult(PtStatus t) => t == null ? null : new(t.Id, t.Name, t.Color);

    [ObjectFactory]
    private static string CreateRealizingUser(ICollection<ProductTarget> t) => t?.FirstOrDefault()?.User == null ? "-" : $"{t.FirstOrDefault().User.Name} ({t.FirstOrDefault().User.Username})";

    [ObjectFactory]
    private static Shared.Models.ProductTemplates.ApiTemplateSelect CreateApiTemplate(long templateId) => new(templateId);

    [MapProperty(nameof(Product.ProductTargets), nameof(ListItemModel.RealizingUser))]
    public partial ListItemModel ToListItemModel(Product prod);

    [MapProperty("TemplateId.Id", nameof(Product.TemplateId))]
    public partial Product ToEntity(FormModel form);

    [MapProperty("TemplateId.Id", nameof(Product.TemplateId))]
    public partial void ToEntity(FormModel form, Product entity);

    public partial DetailsModel ToDetails(Product prod);

    public IQueryable<ListItemModel> ToListItemModel(IQueryable<Product> prod)
    {
        return ToListItemModelAuto(prod.Include(p => p.ProductTargets).ThenInclude(p => p.User));
    }

    public partial IQueryable<ListItemModel> ToListItemModelAuto(IQueryable<Product> prod);

    public partial IQueryable<DetailsModel> ToDetailsModel(IQueryable<Product> query);

    public partial DetailsModel ToDetailsModel(Product model);

    [ObjectFactory]
    private static string CreateCreatedByDescription(User t) => t != null ? t.Name : "-";

    [ObjectFactory]
    private static string CreateUpdatedByDescription(User t) => t != null ? t.Name : "-";
}