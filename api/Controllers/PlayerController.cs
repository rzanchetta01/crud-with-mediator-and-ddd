using application.Mediator.Player;
using domain.Dto;
using domain.Network;
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
        public async Task<ResponseTemplate> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlayerQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<ResponseTemplate> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdPlayerQuery(id), cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseTemplate> Post(PlayerDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreatePlayerCommand(data), cancellationToken);
            return result;
        }

        [HttpDelete]
        public async Task<ResponseTemplate> Delete(PlayerDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePlayerCommand(data), cancellationToken);
            return result;
        }
        
        [HttpPut]
        public async Task<ResponseTemplate> Update(PlayerDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdatePlayerCommand(data), cancellationToken);
            return result;
        }
    }
}
