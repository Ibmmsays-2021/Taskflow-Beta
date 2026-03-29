using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastracture.Identity;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Persistence.EFCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly IMediator _mediator; 
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService, IMediator mediator) : base(options)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
    }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    private readonly ICurrentUserService _currentUserService;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<BaseEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); 
    } 
    ///OutboxPattern
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditEntities();
        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEvents(cancellationToken);
        return result;
    }

    private void UpdateAuditEntities()
    {
        var userId = _currentUserService.CurrentUserId != null ? Guid.Parse(_currentUserService.CurrentUserId) : (Guid?)null;
        foreach (var entry in ChangeTracker.Entries<TrackedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreationDate = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                    entry.Entity.CreatedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    break;
            }
        }
    }

    private async Task DispatchDomainEvents(CancellationToken ct)
    {
        // Get all entities that have events waiting
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                // Publish the event to MediatR
                await _mediator.Publish(domainEvent, ct);
            }

            // Clear events so they don't fire twice if saved again
            entity.ClearDomainEvents();
        }
    }
}
