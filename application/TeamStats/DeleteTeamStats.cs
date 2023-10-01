using domain.Dto;
using domain.Entities;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.TeamStats;

public record DeleteTeamStatsCommand(TeamStatsDto Dto) : IRequest<Unit>;

public class DeleteTeamStatsHandler : IRequestHandler<DeleteTeamStatsCommand, Unit>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public DeleteTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteTeamStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(_mapper.Map(request.Dto), cancellationToken);
        return Unit.Value;
    }
}
