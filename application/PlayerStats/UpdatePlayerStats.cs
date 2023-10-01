using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.PlayerStats;

public record UpdatePlayerStatsCommand(PlayerStatsDto TeamDto) : IRequest<Unit>;

public class UpdatePlayerStatsHandler : IRequestHandler<UpdatePlayerStatsCommand, Unit>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;
    
    public UpdatePlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePlayerStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(_mapper.Map(request.TeamDto), cancellationToken);
        return Unit.Value;
    }
}