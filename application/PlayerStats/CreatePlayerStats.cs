using application.Team;
using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.PlayerStats;

public record CreatePlayerStatsCommand(PlayerStatsDto TeamDto) : IRequest<Unit>;

public class CreatePlayerStatsHandler : IRequestHandler<CreatePlayerStatsCommand, Unit>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public CreatePlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreatePlayerStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.InsertAsync(_mapper.Map(request.TeamDto), cancellationToken);
        return Unit.Value;
    }
}