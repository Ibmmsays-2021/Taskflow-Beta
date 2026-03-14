using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums;

public enum TicketStatus
{
    New = 1,
    InProgress = 2,
    Done = 3,
    ToRecheck = 4,
    ReadyForTesting = 5,
    Testing = 6,
    Closed = 7,
    Resolved = 8,
}
