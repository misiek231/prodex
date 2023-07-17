using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Products;

namespace Prodex.Bussines.MappingProfiles.Products;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ListItemModel>();

        CreateMap<FormModel, Product>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Template, o => o.Ignore());
    }
}
