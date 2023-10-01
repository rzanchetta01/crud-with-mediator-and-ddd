using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.PlayerStats;

public record DeletePlayerStatsCommand(PlayerStatsDto TeamDto) : IRequest<ResponseTemplate>;

public class DeletePlayerStatsHandler : IRequestHandler<DeletePlayerStatsCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public DeletePlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(DeletePlayerStatsCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var entity = _mapper.Map(request.TeamDto);

        try
        {
            await _repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception e)
        {
            errors.Add(e.InnerException?.Message ?? new string("Unknown error"));
        }

        var code = errors.Count > 0 ? HttpStatusCode.BadGateway : HttpStatusCode.OK;
        return new ResponseTemplate(code, errors);
    }
}