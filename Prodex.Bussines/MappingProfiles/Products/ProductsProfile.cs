using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;
using Prodex.Shared.Utils;

namespace Prodex.Bussines.MappingProfiles.Products;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ListItemModel>()
            .ForMember(p => p.Template, o => o.MapFrom(k => new KeyValueResult(k.Template.Id, k.Template.Name)));

        CreateMap<FormModel, Product>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Template, o => o.Ignore());
    }
}
