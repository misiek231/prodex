﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prodex.Data;
using Prodex.Data.Models;
using System;
using System.Collections.Generic;

namespace Prodex.Data.Configurations
{
    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.DateCreatedUtc).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.DateUpdatedUtc).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.GivenName).HasMaxLength(60);
            entity.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(60);
            entity.Property(e => e.Surname).HasMaxLength(60);
            entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(60);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .HasConstraintName("FK_Users_CreatedBy_Users_Id");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .HasConstraintName("FK_Users_UpdatedBy_Users_Id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<User> entity);
    }
}
