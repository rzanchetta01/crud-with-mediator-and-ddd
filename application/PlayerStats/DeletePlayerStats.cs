using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.PlayerStats;

public record DeletePlayerStatsCommand(PlayerStatsDto TeamDto) : IRequest<Unit>;

public class DeletePlayerStatsHandler : IRequestHandler<DeletePlayerStatsCommand, Unit>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public DeletePlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeletePlayerStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(_mapper.Map(request.TeamDto), cancellationToken);
        return Unit.Value;
    }
}