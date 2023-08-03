﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodex.Data;
using Prodex.Data.Models;
using System;
using System.Collections.Generic;

namespace Prodex.Data.Configurations
{
    public partial class DictionaryTermConfiguration : IEntityTypeConfiguration<DictionaryTerm>
    {
        public void Configure(EntityTypeBuilder<DictionaryTerm> entity)
        {
            entity.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(60);

            entity.HasOne(d => d.Dictionary).WithMany(p => p.DictionaryTerms)
            .HasForeignKey(d => d.DictionaryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DictionaryTerms_DictionaryId_Dictionaries_Id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DictionaryTerm> entity);
    }
}
