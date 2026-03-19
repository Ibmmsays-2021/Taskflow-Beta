using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public record TicketResponse(
    Guid Id,
    string Name,
    string Status,
    string Type,
    string AssignedUserName);
