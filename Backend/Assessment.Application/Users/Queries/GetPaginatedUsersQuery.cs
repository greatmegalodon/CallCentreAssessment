using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Extensions;
using Assessment.Shared.Models;
using MediatR;

namespace Assessment.Application.Users.Queries;

public class GetPaginatedUsersQuery : PaginatedRequest<GetPaginatedUsersQueryRow> { }

public class GetPaginatedUsersQueryRow
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}

public class GetPaginatedUsersQueryHandler
    : IRequestHandler<GetPaginatedUsersQuery, PaginatedResult<GetPaginatedUsersQueryRow>>
{
    private readonly ApplicationDbContext _context;

    public GetPaginatedUsersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<GetPaginatedUsersQueryRow>> Handle(
        GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var isDescending = request.SortDirection == SortDirection.Descending;

        if (string.IsNullOrWhiteSpace(request.SortBy))
        {
            isDescending = true;
            request.SortBy = nameof(User.Username);
        }
        
        var query = _context
            .Users
            .Select(x => new GetPaginatedUsersQueryRow
            {
                Id = x.Id,
                Username = x.Username,
            })
            .IfNotEmptyThenWhere(
                request.SearchValue,
                x => x.Username.Contains(request.SearchValue!.Trim()))
            .OrderBy(request.SortBy, isDescending);

        return await query.PaginateAsync(request);
    }
}
