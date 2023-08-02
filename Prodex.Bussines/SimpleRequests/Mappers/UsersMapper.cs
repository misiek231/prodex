using Prodex.Bussines.Services;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.MappingProfiles.Users;

[Mapper]
public partial class UsersMapper : IListMapper<User, ListItemModel>, ICreateMapper<User, FormModel>, IUpdateMapper<User, FormModel>, IDetailsMapper<User, FormModel>
{
    private readonly PasswordHasher Hasher;

    public UsersMapper(PasswordHasher hasher)
    {
        Hasher = hasher;
    }

    public partial ListItemModel ToListItemModel(User status);

    public User ToEntity(FormModel form)
    {
        var user = CustomToEntity(form);
        user.Password = Hasher.HashPassword(user.Password);
        return user;
    }

    public partial User CustomToEntity(FormModel form);

    public void ToEntity(FormModel form, User user)
    {
        CustomToEntity(form, user);
        user.Password = Hasher.HashPassword(user.Password);
    }

    public partial void CustomToEntity(FormModel form, User user);

    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<User> prod);

    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<User> query);
}