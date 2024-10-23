using Assessment.Data.Entities;
using Assessment.Data;
using Assessment.Shared.Models;
using MediatR;
using Assessment.Shared.Extensions;

namespace Assessment.Application.Stats.Queries;

public class GetPaginatedStatsQuery : PaginatedRequest<GetPaginatedStatsQueryRow> { }

public class GetPaginatedStatsQueryRow
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTimeOffset DateCallStarted { get; set; }
}

public class GetPaginatedStatsQueryHandler
    : IRequestHandler<GetPaginatedStatsQuery, PaginatedResult<GetPaginatedStatsQueryRow>>
{
    private readonly ApplicationDbContext _context;

    public GetPaginatedStatsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<GetPaginatedStatsQueryRow>> Handle(
        GetPaginatedStatsQuery request,
        CancellationToken cancellationToken)
    {
        var isDescending = request.SortDirection == SortDirection.Descending;

        if (string.IsNullOrWhiteSpace(request.SortBy))
        {
            isDescending = false;
            request.SortBy = nameof(Call.DateCallStarted);
        }

        var query = _context
            .Calls
            .Join(_context.Users, c => c.CallingUserId, u => u.Id,
            (call, user) => new
            {
                call.Id,
                user.Username,
                call.DateCallStarted,
            })
            .Select(x => new GetPaginatedStatsQueryRow
            {
                Id = x.Id,
                Username = x.Username,
                DateCallStarted = x.DateCallStarted,
            })
            .IfNotEmptyThenWhere(
                DateTime.Today,
                x => x.DateCallStarted.Date == DateTime.Today)
            .OrderBy(request.SortBy, isDescending);

        return await query.PaginateAsync(request);
    }
}
