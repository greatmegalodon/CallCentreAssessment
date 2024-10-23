using Assessment.Data;
using Assessment.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Application.Calls.Queries;

public class GetUsersListQuery : IRequest<Result<GetUsersListResult>> { }

public class GetUsersListResult
{
    public List<ListItem> Items { get; set; }
}

public class ListItem
{
    public Guid Id { get; set; }
    
    public string Value { get; set; }
}

public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, Result<GetUsersListResult>>
{
    private readonly ApplicationDbContext _context;

    public GetUsersListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetUsersListResult>> Handle(
        GetUsersListQuery request,
        CancellationToken cancellationToken)
    {
        var result = new GetUsersListResult
        {
            Items = await _context
                .Users
                .Select(x => new ListItem
                {
                    Id = x.Id,
                    Value = x.Username,
                })
                .ToListAsync(cancellationToken: cancellationToken)
        };

        return Result<GetUsersListResult>.Success(result);
    }
}