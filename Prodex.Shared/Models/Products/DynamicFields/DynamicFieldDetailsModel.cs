﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Shared.Models.Products.DynamicFields;

public class DynamicFieldDetailsModel
{
    public long FieldConfigId {  get; set; }
    public string DisplayName {  get; set; }
    public string Value { get; set; }
}
