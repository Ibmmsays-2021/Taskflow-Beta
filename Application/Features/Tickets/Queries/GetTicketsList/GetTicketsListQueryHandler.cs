

using Application.Common;
using Application.DTOs;
using Application.Features.Tickets.GetTicketsList;
using Dapper; 
using MediatR;
using System.Data;

namespace Application.Features.Tickets.Queries.GetTicketsList;

public class GetTicketsListQueryHandler(IDbConnection dbConnection)
    : IRequestHandler<GetTicketsListQuery, PagedResponse<TicketResponse>>
{
    public async Task<PagedResponse<TicketResponse>> Handle(GetTicketsListQuery request, CancellationToken ct)
    {
        var offset = (request.PageNumber - 1) * request.PageSize;
         
        const string sql = @"
            SELECT COUNT(*) FROM Tickets WHERE IsDeleted = 0;

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
            WHERE t.IsDeleted = 0
            ORDER BY t.Id 
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

        using var multi = await dbConnection.QueryMultipleAsync(sql, new { Offset = offset, PageSize = request.PageSize });

        var totalCount = await multi.ReadFirstAsync<int>();
        var items = await multi.ReadAsync<TicketResponse>();

        return new PagedResponse<TicketResponse>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
