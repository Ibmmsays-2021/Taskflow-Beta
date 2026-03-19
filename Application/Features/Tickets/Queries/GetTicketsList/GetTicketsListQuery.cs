using Application.Common;
using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tickets.GetTicketsList;

public record GetTicketsListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PagedResponse<TicketResponse>>;
