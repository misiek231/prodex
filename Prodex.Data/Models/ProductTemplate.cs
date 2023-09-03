﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Prodex.Data.Models;

public partial class ProductTemplate
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string ProcessXml { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<PtStatus> PtStatuses { get; } = new List<PtStatus>();

    public virtual ICollection<ServiceTaskConfig> ServiceTaskConfigs { get; } = new List<ServiceTaskConfig>();
}