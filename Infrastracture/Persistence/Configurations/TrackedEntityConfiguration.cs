using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Persistence.Configurations;

public class TrackedEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : TrackedEntity
{
    public void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.Property(e => e.CreationDate)
            .IsRequired();
        builder.HasQueryFilter(a => !a.IsDeleted);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);
         
        builder.Property(e => e.CreatedBy)
            .IsRequired(false);

        builder.Property(e => e.ModifiedBy)
            .IsRequired(false);
    }
}
