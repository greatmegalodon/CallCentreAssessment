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
    public required string Username { get; set; }
    public DateTimeOffset MostCallsDate { get; set; }
    public double AverageCallsPerDay { get; set; }
    public double AverageCallsPerUser { get; set; }
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

        var callsData = _context.Calls
            .Join(_context.Users, c => c.CallingUserId, u => u.Id,
            (call, user) => new
            {
                call.Id,
                user.Username,
                call.DateCallStarted,
            });

        var dailyStats = callsData
            .GroupBy(x => x.DateCallStarted.Date)
            .Select(g => new
            {
                Date = g.Key,
                TotalCalls = g.Count(),
                Users = g.Select(x => x.Username).Distinct()
            }).ToList();

        var userStats = callsData
            .GroupBy(x => x.Username)
            .Select(g => new
            {
                Username = g.Key,
                TotalCalls = g.Count()
            }).ToList();

        var averageCallsPerDay = dailyStats.Any() ? dailyStats.Average(x => x.TotalCalls) : 0;
        var averageCallsPerUser = userStats.Any() ? userStats.Average(x => x.TotalCalls) : 0;
        var mostCallsDate = dailyStats.OrderByDescending(x => x.TotalCalls).FirstOrDefault();

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
                AverageCallsPerDay = averageCallsPerDay,
                AverageCallsPerUser = averageCallsPerUser,
                MostCallsDate = mostCallsDate.Date
            })
            .IfNotEmptyThenWhere(
                DateTime.Today,
                x => x.DateCallStarted.Date == DateTime.Today)
            .OrderBy(request.SortBy, isDescending);

        return await query.PaginateAsync(request);
    }
}
