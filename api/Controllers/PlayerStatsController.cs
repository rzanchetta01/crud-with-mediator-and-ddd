
using application.Mediator.PlayerStats;
using domain.Dto;
using domain.Network;
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
        public async Task<ResponseTemplate> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlayerStatsQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<ResponseTemplate> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdPlayerStatsQuery(id), cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseTemplate> Post(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreatePlayerStatsCommand(data), cancellationToken);
            return result;
        }

        [HttpDelete]
        public async Task<ResponseTemplate> Delete(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePlayerStatsCommand(data), cancellationToken);
            return result;
        }
        
        [HttpPut]
        public async Task<ResponseTemplate> Update(PlayerStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdatePlayerStatsCommand(data), cancellationToken);
            return result;
        }
    }
}
