using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates.Statuses;

namespace Prodex.Bussines.MappingProfiles.ProductTemplates;

public class PtStatusesProfile : Profile
{
    public PtStatusesProfile()
    {
        CreateMap<PtStatus, ListItemModel>();

        CreateMap<FormModel, PtStatus>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Template, o => o.Ignore())
            .ReverseMap();
    }
}
