using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events;

public class TicketCreatedEvent: BaseEvent
{
    public Ticket Ticket { get; }

    public TicketCreatedEvent(Ticket ticket)
    {
        Ticket = ticket;
    }
}
