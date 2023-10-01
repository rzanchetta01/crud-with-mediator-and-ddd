using domain.Dto;
using domain.Entities;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.TeamStats;

public record CreateTeamStatsCommand(TeamStatsDto Dto) : IRequest<Unit>;

public class CreateTeamStatsHandler : IRequestHandler<CreateTeamStatsCommand, Unit>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;
    
    public CreateTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateTeamStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.InsertAsync(_mapper.Map(request.Dto), cancellationToken);
        return Unit.Value;
    }
}
