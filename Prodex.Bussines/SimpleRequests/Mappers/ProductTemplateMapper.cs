using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class ProductTemplateMapper :
    IListMapper<ProductTemplate, ListItemModel>,
    ICreateMapper<ProductTemplate, FormModel>,
    IUpdateMapper<ProductTemplate, FormModel>,
    IDetailsMapper<ProductTemplate, FormModel>
{
    public partial ListItemModel ToListItemModel(ProductTemplate entity);
    public partial FormModel ToForm(ProductTemplate form);
    public partial ProductTemplate ToEntity(FormModel form);
    public partial void ToEntity(FormModel form, ProductTemplate entity);
    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<ProductTemplate> prod);
    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<ProductTemplate> query);
    public partial FormModel ToDetailsModel(ProductTemplate model);
}