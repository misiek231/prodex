using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products.History;
using Prodex.Shared.Utils;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.SimpleRequests.Mappers;

[Mapper]
public partial class HistoryMapper : IListMapper<History, ListItemModel>
{
    [ObjectFactory]
    private static KeyValueResult CreateKeyValueResult(User t) => new(t.Id, t.GivenName + " " + t.Surname);

    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<History> query);
}