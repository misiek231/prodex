﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Prodex.Data.Models;

public partial class DictionaryTerm
{
    public long Id { get; set; }

    public string Value { get; set; }

    public long DictionaryId { get; set; }

    public virtual Dictionary Dictionary { get; set; }
}