using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging; 

namespace Application.Features.Tickets.EventHandlers;

public class TicketCreatedHandler(ILogger<TicketCreatedHandler> logger) 
    : INotificationHandler<TicketCreatedEvent>
{
    public Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        var ticket = notification.Ticket;

        logger.LogInformation("🚀 [EVENT] A new ticket was created: {TicketName} (ID: {TicketId})",
            ticket.Name, ticket.Id);

        return Task.CompletedTask;
    }
}
