﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Prodex.Data.Models;

public partial class PtStatus
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Color { get; set; }

    public long TemplateId { get; set; }

    public virtual ProductTemplate Template { get; set; }
}