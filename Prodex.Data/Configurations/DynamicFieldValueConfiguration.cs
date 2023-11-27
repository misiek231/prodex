﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodex.Data;
using Prodex.Data.Models;
using System;
using System.Collections.Generic;

namespace Prodex.Data.Configurations
{
    public partial class DynamicFieldValueConfiguration : IEntityTypeConfiguration<DynamicFieldValue>
    {
        public void Configure(EntityTypeBuilder<DynamicFieldValue> entity)
        {
            entity.Property(e => e.DateCreatedUtc).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DateUpdatedUtc).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Value).IsRequired();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DynamicFieldValueCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .HasConstraintName("FK_DynamicFieldValues_CreatedBy_Users_Id");

            entity.HasOne(d => d.FieldConfig).WithMany(p => p.DynamicFieldValues)
            .HasForeignKey(d => d.FieldConfigId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DynamicFieldValues_FieldConfigId_FieldConfigs_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.DynamicFieldValues)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DynamicFieldValues_ProductId_Products_Id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DynamicFieldValueUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .HasConstraintName("FK_DynamicFieldValues_UpdatedBy_Users_Id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DynamicFieldValue> entity);
    }
}
