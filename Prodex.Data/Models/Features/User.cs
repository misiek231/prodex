using Prodex.Data.Interfaces;

namespace Prodex.Data.Models;

public partial class User : IEntity
{
    public string Name => GivenName + " " + Surname;

}