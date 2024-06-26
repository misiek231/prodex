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

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime DateCreatedUtc { get; set; }

    public DateTime DateUpdatedUtc { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual Dictionary Dictionary { get; set; }

    public virtual User UpdatedByNavigation { get; set; }

    public virtual ICollection<SequenceFlowConfig> LSequenceFlowConfigs { get; } = new List<SequenceFlowConfig>();
    public virtual ICollection<SequenceFlowConfig> RSequenceFlowConfigs { get; } = new List<SequenceFlowConfig>();
}