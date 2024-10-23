using Assessment.Application.Calls.Commands;
using Assessment.Application.Stats.Queries;

namespace Assessment.Api.Controllers
{
    public class StatsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedResult<GetPaginatedStatsQueryRow>> GetPaginatedStatsAsync(
            [FromQuery] GetPaginatedStatsQuery query) =>
            await _mediator.Send(query);

        [HttpPost]
        public async Task<Result<CreateCallResult>> CreateCallAsync(
            [FromBody] CreateCallCommand command) =>
            await _mediator.Send(command);
    }
}
