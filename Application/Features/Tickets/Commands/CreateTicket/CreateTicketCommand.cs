using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tickets.Commands.CreateTicket;

public record CreateTicketCommand(string Name, TicketType Type, int BoardId) : IRequest<Guid>;
