using Assessment.Data;
using Assessment.Data.Entities;
using Assessment.Shared.Extensions;
using Assessment.Shared.Models;
using MediatR;

namespace Assessment.Application.Calls.Queries;

public class GetPaginatedCallsQuery : PaginatedRequest<GetPaginatedCallsQueryRow> { }

public class GetPaginatedCallsQueryRow
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTimeOffset DateCallStarted { get; set; }
}

public class GetPaginatedCallsQueryHandler
    : IRequestHandler<GetPaginatedCallsQuery, PaginatedResult<GetPaginatedCallsQueryRow>>
{
    private readonly ApplicationDbContext _context;

    public GetPaginatedCallsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<GetPaginatedCallsQueryRow>> Handle(
        GetPaginatedCallsQuery request,
        CancellationToken cancellationToken)
    {
        var isDescending = request.SortDirection == SortDirection.Descending;

        if (string.IsNullOrWhiteSpace(request.SortBy))
        {
            isDescending = true;
            request.SortBy = nameof(Call.DateCallStarted);
        }
        
        var query = _context
            .Calls
            .Select(x => new GetPaginatedCallsQueryRow
            {
                Id = x.Id,
                Username = x.CallingUser.Username,
                DateCallStarted = x.DateCallStarted,
            })
            .IfNotEmptyThenWhere(
                request.SearchValue,
                x => x.Username.Contains(request.SearchValue!.Trim()))
            .OrderBy(request.SortBy, isDescending);

        return await query.PaginateAsync(request);
    }
}
