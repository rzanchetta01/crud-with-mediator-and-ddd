using application.TeamStats;
using domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamStatsController : ControllerBase
    {private readonly IMediator _mediator;
     
        public TeamStatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TeamStatsDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllTeamStatsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TeamStatsDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdTeamStatsQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(TeamStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateTeamStatsCommand(data), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(TeamStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTeamStatsCommand(data), cancellationToken);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(TeamStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateTeamStatsCommand(data), cancellationToken);
            return Ok();
        }
    }
}
