using application.Mediator.Team;
using domain.Dto;
using domain.Network;
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
        public async Task<ResponseTemplate> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllTeamQuery(), cancellationToken);
            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<ResponseTemplate> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdTeamQuery(id), cancellationToken);
            return result;
        }

        [HttpPost]
        public async Task<ResponseTemplate> Post(TeamDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateTeamCommand(data), cancellationToken);
            return result;
        }

        [HttpDelete]
        public async Task<ResponseTemplate> Delete(TeamDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteTeamCommand(data), cancellationToken);
            return result;
        }
        
        [HttpPut]
        public async Task<ResponseTemplate> Update(TeamDto data, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateTeamCommand(data), cancellationToken);
            return result;
        }
    }
}
