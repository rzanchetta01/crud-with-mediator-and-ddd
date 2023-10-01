
using application.PlayerStats;
using domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IMediator _mediator;
     
        public PlayerStatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PlayerStatsDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlayerStatsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PlayerStatsDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdPlayerStatsQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreatePlayerStatsCommand(data), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletePlayerStatsCommand(data), cancellationToken);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdatePlayerStatsCommand(data), cancellationToken);
            return Ok();
        }
    }
}
