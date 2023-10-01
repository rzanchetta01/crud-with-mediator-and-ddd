using application.Player;
using domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
     
        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PlayerDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlayerQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PlayerDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdPlayerQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PlayerDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreatePlayerCommand(data), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(PlayerDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletePlayerCommand(data), cancellationToken);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(PlayerDto data, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdatePlayerCommand(data), cancellationToken);
            return Ok();
        }
    }
}
