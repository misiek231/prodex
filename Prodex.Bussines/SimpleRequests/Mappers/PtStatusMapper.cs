using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.Statuses;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class PtStatusMapper : IListMapper<PtStatus, ListItemModel>, ICreateMapper<PtStatus, FormModel>, IUpdateMapper<PtStatus, FormModel>, IDetailsMapper<PtStatus, FormModel>
{
    public partial ListItemModel ToListItemModel(PtStatus status);
    public partial FormModel ToForm(PtStatus entity);
    public partial PtStatus ToEntity(FormModel form);
    public partial void ToEntity(FormModel form, PtStatus entity);
    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<PtStatus> prod);
    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<PtStatus> query);
    public partial FormModel ToDetailsModel(PtStatus model);
}