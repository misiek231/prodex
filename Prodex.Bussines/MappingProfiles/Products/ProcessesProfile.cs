using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.MappingProfiles.Products;

public class ProcessesProfile : Profile
{
    public ProcessesProfile()
    {
        CreateMap<Product, ListItemModel>()
            .ForMember(p => p.Process, o => o.MapFrom(k => new KeyValueResult(k.Process.Id, k.Process.Name)));

        CreateMap<FormModel, Product>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Process, o => o.Ignore());
    }
}
