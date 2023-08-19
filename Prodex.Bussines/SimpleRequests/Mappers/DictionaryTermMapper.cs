using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.DictionaryTerms;
using Prodex.Shared.Utils;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class DictionaryTermMapper : 
    IListMapper<DictionaryTerm, ListItemModel>, 
    ICreateMapper<DictionaryTerm, FormModel>, 
    IUpdateMapper<DictionaryTerm, FormModel>, 
    IDetailsMapper<DictionaryTerm, FormModel>
{

    [ObjectFactory]
    private static KeyValueResult CreateKeyValueResult(Dictionary t) => new(t.Id, t.Name);

    [ObjectFactory]
    private static Shared.Models.Dictionary.ApiDictionarySelect CreateApiDictionary(long dictionaryId) => new(dictionaryId);

    public partial ListItemModel ToListItemModel(DictionaryTerm status);
    public partial FormModel ToForm(DictionaryTerm entity);

    [MapProperty("DictionaryId.Id", nameof(DictionaryTerm.DictionaryId))]
    public partial DictionaryTerm ToEntity(FormModel form);

    [MapProperty("DictionaryId.Id", nameof(DictionaryTerm.DictionaryId))]
    public partial void ToEntity(FormModel form, DictionaryTerm entity);
    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<DictionaryTerm> prod);
    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<DictionaryTerm> query);

    public partial FormModel ToDetailsModel(DictionaryTerm model);
}