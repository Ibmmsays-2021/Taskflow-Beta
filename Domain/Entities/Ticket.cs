using Domain.Enums;
using Domain.Events;
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
        AddDomainEvent(new TicketCreatedEvent(this));
    }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public TicketType Type { get; set; }
    public TicketStatus Status { get; set; }
    public Guid? AssignedToUser { get; set; } 
    public int BoardId { get; set; }

    // Navigation Properties
    public Board Board { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; }
    public Guid? ParentTicketId { get; set; }
    public Ticket? ParentTicket { get; set; }
    public ICollection<Ticket> SubTickets { get; set; } = new List<Ticket>();

    public void MoveToInProgress()
    {
        if (Status == TicketStatus.Done) throw new InvalidOperationException("Cannot reopen closed tickets.");
        Status = TicketStatus.InProgress;
    }
    public void MoveToDone()
    {
        if (Status == TicketStatus.New) throw new InvalidOperationException("Cannot close tickets that haven't been started.");
        Status = TicketStatus.Done;
    }
     public void Reopen()
    {
        if (Status != TicketStatus.Done) throw new InvalidOperationException("Only closed tickets can be reopened.");
        Status = TicketStatus.InProgress;
    }
    public void AssignToUser(Guid userId)
    {
        AssignedToUser = userId;
    }
    public void UnassignUser()
    {
        AssignedToUser = null;
    }
    public void AddAttachment(Attachment attachment)
    {
        if (attachment == null) throw new ArgumentNullException(nameof(attachment));
        Attachments.Add(attachment);
    }
     public void RemoveAttachment(Attachment attachment)
    {
        if (attachment == null) throw new ArgumentNullException(nameof(attachment));
        Attachments.Remove(attachment);
    }
    public void AddSubTicket(Ticket subTicket)
    {
        if (subTicket == null) throw new ArgumentNullException(nameof(subTicket));
        subTicket.ParentTicketId = this.Id;
        SubTickets.Add(subTicket);
    }
     public void RemoveSubTicket(Ticket subTicket)
    {
        if (subTicket == null) throw new ArgumentNullException(nameof(subTicket));
        subTicket.ParentTicketId = null;
        SubTickets.Remove(subTicket);
    }
 }
