﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Prodex.Data.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public int UserType { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime DateCreatedUtc { get; set; }

    public DateTime DateUpdatedUtc { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual ICollection<Dictionary> DictionaryCreatedByNavigations { get; } = new List<Dictionary>();

    public virtual ICollection<DictionaryTerm> DictionaryTermCreatedByNavigations { get; } = new List<DictionaryTerm>();

    public virtual ICollection<DictionaryTerm> DictionaryTermUpdatedByNavigations { get; } = new List<DictionaryTerm>();

    public virtual ICollection<Dictionary> DictionaryUpdatedByNavigations { get; } = new List<Dictionary>();

    public virtual ICollection<DynamicFieldValue> DynamicFieldValueCreatedByNavigations { get; } = new List<DynamicFieldValue>();

    public virtual ICollection<DynamicFieldValue> DynamicFieldValueUpdatedByNavigations { get; } = new List<DynamicFieldValue>();

    public virtual ICollection<FieldConfig> FieldConfigCreatedByNavigations { get; } = new List<FieldConfig>();

    public virtual ICollection<FieldConfig> FieldConfigUpdatedByNavigations { get; } = new List<FieldConfig>();

    public virtual ICollection<History> Histories { get; } = new List<History>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; } = new List<User>();

    public virtual ICollection<Product> ProductCreatedByNavigations { get; } = new List<Product>();

    public virtual ICollection<ProductTarget> ProductTargets { get; } = new List<ProductTarget>();

    public virtual ICollection<ProductTemplate> ProductTemplateCreatedByNavigations { get; } = new List<ProductTemplate>();

    public virtual ICollection<ProductTemplate> ProductTemplateUpdatedByNavigations { get; } = new List<ProductTemplate>();

    public virtual ICollection<Product> ProductUpdatedByNavigations { get; } = new List<Product>();

    public virtual ICollection<PtStatus> PtStatusCreatedByNavigations { get; } = new List<PtStatus>();

    public virtual ICollection<PtStatus> PtStatusUpdatedByNavigations { get; } = new List<PtStatus>();

    public virtual ICollection<SendTaskConfig> SendTaskConfigs { get; } = new List<SendTaskConfig>();

    public virtual ICollection<ServiceTaskConfig> ServiceTaskConfigCreatedByNavigations { get; } = new List<ServiceTaskConfig>();

    public virtual ICollection<ServiceTaskConfig> ServiceTaskConfigUpdatedByNavigations { get; } = new List<ServiceTaskConfig>();

    public virtual User UpdatedByNavigation { get; set; }
}