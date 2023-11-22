using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Users;

public enum UserType
{
    [Description("Wybierz...")]
    None,

    [Description("Administrator")]
    Admin,

    [Description("Pracownik")]
    Worker
}
