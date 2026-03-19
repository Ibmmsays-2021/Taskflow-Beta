using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tickets.Queries.GetTicketById.GetTicketByIdQuery;

public record GetTicketByIdQuery(Guid Id) : IRequest<TicketResponse?>;

