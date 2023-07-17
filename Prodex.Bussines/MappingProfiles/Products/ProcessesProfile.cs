using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.MappingProfiles.Products;

public class ProcessesProfile : Profile
{
    public ProcessesProfile()
    {
        CreateMap<ProductTemplate, ListItemModel>();

        CreateMap<FormModel, ProductTemplate>()
            .ForMember(p => p.Id, o => o.Ignore());
    }
}
