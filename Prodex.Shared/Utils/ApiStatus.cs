using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Utils;

public class ApiStatus : KeyValueResult
{
    public string Color { get; set; }

    public ApiStatus() : base(0, "") { }

    public ApiStatus(long key, string value) : base(key, value)
    {
    }

    public ApiStatus(long key, string value, string color) : base(key, value)
    {
        Color = color;
    }
}
