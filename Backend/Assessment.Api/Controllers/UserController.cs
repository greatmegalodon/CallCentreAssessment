using Assessment.Application.Calls.Queries;
using Assessment.Application.Users.Commands;
using Assessment.Application.Users.Queries;

namespace Assessment.Api.Controllers;

public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<PaginatedResult<GetPaginatedUsersQueryRow>> GetPaginatedUsersAsync(
        [FromQuery] GetPaginatedUsersQuery query) =>
        await _mediator.Send(query);
    
    [HttpGet("list")]
    public async Task<Result<GetUsersListResult>> GetUsersListAsync(
        [FromQuery] GetUsersListQuery query) =>
        await _mediator.Send(query);
    
    [HttpPost]
    public async Task<Result<CreateUserResult>> CreateUserAsync(
        [FromBody] CreateUserCommand command) =>
        await _mediator.Send(command);
}