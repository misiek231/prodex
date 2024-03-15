using Prodex.Bussines.Services;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Data.Models;
using Prodex.Shared.Models.Users;
using Prodex.Shared.Utils;
using Prodex.Utils;
using Riok.Mapperly.Abstractions;

namespace Prodex.Bussines.Mappers;

[Mapper]
public partial class UsersMapper : 
    IListMapper<User, ListItemModel>, 
    ICreateMapper<User, FormModel>, 
    IUpdateMapper<User, FormModel>, 
    IDetailsMapper<User, FormModel>
{
    public partial ListItemModel ToListItemModel(User user);

    private readonly PasswordHasher Hasher;

    public UsersMapper(PasswordHasher hasher)
    {
        Hasher = hasher;
    }


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

    [ObjectFactory]
    private static string CreateUserTypeDescription(UserType t) => t == UserType.None ? "-" : t.Description();

    [ObjectFactory]
    private static string CreateCreatedByDescription(User t) => t != null ? t.Name : "-";

    [ObjectFactory]
    private static string CreateUpdatedByDescription(User t) => t != null ? t.Name : "-";

    public partial IQueryable<ListItemModel> ToListItemModel(IQueryable<User> prod);

    [MapperIgnoreSource(nameof(User.Password))]
    public partial IQueryable<FormModel> ToDetailsModel(IQueryable<User> query);

    [MapperIgnoreSource(nameof(User.Password))]
    public partial FormModel ToDetailsModel(User model);
}