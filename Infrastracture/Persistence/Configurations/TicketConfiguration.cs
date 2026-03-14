using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infrastracture.Persistence.Configurations;

public class TicketConfiguration : TrackedEntityConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        base.Configure(builder);

        builder.ToTable("Tickets");

        // Primary Key
        builder.HasKey(t => t.Id);
         
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(2000);
         
        builder.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();
         
        builder.HasDiscriminator(t => t.Type);
         
        builder.HasOne(t => t.ParentTicket)
            .WithMany(t => t.SubTickets)
            .HasForeignKey(t => t.ParentTicketId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(t => t.Attachments)
            .WithOne(a => a.Ticket)
            .HasForeignKey(a => a.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasDiscriminator(t => t.Type)
            .HasValue<Ticket>(TicketType.Task)
            .HasValue<Ticket>(TicketType.Bug)
            .HasValue<Ticket>(TicketType.Epic)
            .HasValue<Ticket>(TicketType.Userstory);
    }
}

