﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Prodex.Data.Models;

public partial class Dictionary
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<DictionaryTerm> DictionaryTerms { get; } = new List<DictionaryTerm>();
}