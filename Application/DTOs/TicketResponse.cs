using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public record TicketResponse(
    Guid Id,
    string Name,
    string? Description,
    int Type,              
    int Status,            
    int BoardId,
    Guid? ParentTicketId,
    string? AssignedToUser);  
