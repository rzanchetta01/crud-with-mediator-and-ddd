using application.Team;
using domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
     
        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TeamDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllTeamQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TeamDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdTeamQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(TeamDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateTeamCommand(data), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(TeamDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTeamCommand(data), cancellationToken);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(TeamDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateTeamCommand(data), cancellationToken);
            return Ok();
        }
    }
}
