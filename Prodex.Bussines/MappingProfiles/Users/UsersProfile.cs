using AutoMapper;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;

namespace Prodex.Bussines.MappingProfiles.Users;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, ListItemModel>();

        CreateMap<FormModel, User>()
            .ForMember(p => p.Id, o => o.Ignore())
            .ForMember(p => p.Password, o => o.Ignore());
    }
}
