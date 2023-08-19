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
    private static KeyValueResult CreateKeyValueResult(ProductTemplate t) => new(t.Id, t.Name);

    [ObjectFactory]
    private static Shared.Models.ProductTemplates.ApiTemplateSelect CreateApiTemplate(long templateId) => new(templateId);

    public partial ListItemModel ToListItemModel(Product prod);

    [MapProperty("TemplateId.Id", nameof(Product.TemplateId))]
    public partial Product ToEntity(FormModel form);

    [MapProperty("TemplateId.Id", nameof(Product.TemplateId))]
    public partial void ToEntity(FormModel form, Product entity);

    public partial DetailsModel ToDetails(Product prod);

    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<Product> prod);

    public partial IQueryable<DetailsModel> ToDetailsModel(IQueryable<Product> query);

    public partial DetailsModel ToDetailsModel(Product model);
}