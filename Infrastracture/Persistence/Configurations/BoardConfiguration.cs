using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastracture.Persistence.Configurations;

public class BoardConfiguration : TrackedEntityConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        base.Configure(builder);
        builder.ToTable("Boards");
         
        builder.HasKey(t => t.Id);
        builder.HasQueryFilter(a => !a.IsDeleted);

        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.HasMany(b => b.Tickets)
           .WithOne(t => t.Board)
           .HasForeignKey(t => t.BoardId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
