using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Users;

public class ListItemModel : ConfidentialInfo
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string UserTypeEnum { get; set; }
}
