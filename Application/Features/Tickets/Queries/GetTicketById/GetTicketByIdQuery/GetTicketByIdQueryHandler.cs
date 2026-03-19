using Application.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Features.Tickets.Queries.GetTicketById.GetTicketByIdQuery;

public class GetTicketByIdQueryHandler(IDbConnection dbConnection)
    : IRequestHandler<GetTicketByIdQuery, TicketResponse?>
{
    public async Task<TicketResponse?> Handle(GetTicketByIdQuery request, CancellationToken ct)
    {
        const string sql = @"
                SELECT t.Id, 
                       t.Name, 
                       t.Description, 
                       t.Type, 
                       t.Status, 
                       t.BoardId, 
                       t.ParentTicketId, 
                       u.UserName as AssignedToUser  
                FROM Tickets t
                LEFT JOIN AspNetUsers u ON t.AssignedToUser = u.Id
                WHERE t.Id = @Id AND t.IsDeleted = 0";
        return await dbConnection.QueryFirstOrDefaultAsync<TicketResponse>(sql, new { Id = request.Id });
    }
}
