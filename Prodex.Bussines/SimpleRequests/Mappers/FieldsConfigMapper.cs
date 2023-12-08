using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.FieldsConfig;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class FieldsConfigMapper : ICreateMapper<FieldConfig, FieldModel>, IListMapper<FieldConfig, FieldModel>
{
    [MapProperty("Dictionary.Id", "DictionaryId")]
    [MapperIgnoreTarget("Dictionary")]
    public partial FieldConfig ToEntity(FieldModel form);

    [MapProperty("Dictionary.Id", "DictionaryId")]
    [MapperIgnoreTarget("Dictionary")]
    public partial IEnumerable<FieldConfig> ToEntity(IEnumerable<FieldModel> form);
    public partial IQueryable<FieldModel> ToListItemModel(IQueryable<FieldConfig> query);

    [ObjectFactory]
    private static string CreateCreatedByDescription(User t) => t != null ? t.Name : "-";

    [ObjectFactory]
    private static string CreateUpdatedByDescription(User t) => t != null ? t.Name : "-";
}

