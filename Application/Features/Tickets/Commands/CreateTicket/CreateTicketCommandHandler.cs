using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketCommand, Guid>
{
    public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    { 
        var ticket = new Ticket(request.Name, request.Type, request.BoardId);
         
        await unitOfWork.Repository<Ticket>().AddAsync(ticket, cancellationToken);
         
        await unitOfWork.SaveChangesAsync(cancellationToken);
         
        return ticket.Id;
    }
}
