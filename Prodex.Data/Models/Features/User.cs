using Prodex.Data.Interfaces;
using Prodex.Shared.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prodex.Data.Models;

public partial class User : IEntity, IConfidential
{
    public string Name => GivenName + " " + Surname;

    [NotMapped]
    public UserType UserTypeEnum { get => (UserType)UserType; set => UserType = (int)value; }

}