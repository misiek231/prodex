using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.ProductTemplates;

namespace Prodex.Bussines.MappingProfiles.ProductTemplates;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<ProductTemplate, ListItemModel>();

        CreateMap<FormModel, ProductTemplate>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Products, o => o.Ignore());
    }
}
