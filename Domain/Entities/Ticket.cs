using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public sealed class Ticket: TrackedEntity
{
    private Ticket() { }
    public Ticket(string name, TicketType type, int boardId)
    {
        Name = name;
        Type = type;
        BoardId = boardId;
        Status = TicketStatus.New;
        // Validation logic could go here
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public Guid? AssignedToUser { get; set; } 
    public int BoardId { get; set; }
    public Board Board { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; }
    public int? ParentTicketId { get; set; }
    public Ticket? ParentTicket { get; set; }
    public ICollection<Ticket> SubTickets { get; set; } = new List<Ticket>();
}
