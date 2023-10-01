using application.Mediator.TeamStats;
using domain.Dto;
using domain.Network;
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
        public async Task<ResponseTemplate> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllTeamStatsQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<ResponseTemplate> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdTeamStatsQuery(id), cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseTemplate> Post(TeamStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateTeamStatsCommand(data), cancellationToken);
            return result;
        }

        [HttpDelete]
        public async Task<ResponseTemplate> Delete(TeamStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteTeamStatsCommand(data), cancellationToken);
            return result;
        }
        
        [HttpPut]
        public async Task<ResponseTemplate> Update(TeamStatsDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateTeamStatsCommand(data), cancellationToken);
            return result;
        }
    }
}
