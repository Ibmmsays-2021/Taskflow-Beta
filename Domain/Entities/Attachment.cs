using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class Attachment : TrackedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public string FileSize { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}
