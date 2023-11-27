using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Dictionary;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class DictionaryMapper : 
    IListMapper<Dictionary, ListItemModel>, 
    ICreateMapper<Dictionary, FormModel>, 
    IUpdateMapper<Dictionary, FormModel>, 
    IDetailsMapper<Dictionary, FormModel>
{
    public partial ListItemModel ToListItemModel(Dictionary status);
    public partial FormModel ToForm(Dictionary entity);
    public partial Dictionary ToEntity(FormModel form);
    public partial void ToEntity(FormModel form, Dictionary entity);
    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<Dictionary> prod);
    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<Dictionary> query);
    public partial FormModel ToDetailsModel(Dictionary model);

    [ObjectFactory]
    private static string CreateCreatedByDescription(User t) => t != null ? t.Name : "-";

    [ObjectFactory]
    private static string CreateUpdatedByDescription(User t) => t != null ? t.Name : "-";
}