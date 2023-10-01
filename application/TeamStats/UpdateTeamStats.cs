using domain;
using domain.Dto;
using domain.Entities;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.TeamStats;

public record UpdateTeamStatsCommand(TeamStatsDto TeamStatsDtoDto) : IRequest<Unit>;

public class UpdateTeamStatsHandler : IRequestHandler<UpdateTeamStatsCommand, Unit>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public UpdateTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<Unit> Handle(UpdateTeamStatsCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(request.TeamStatsDtoDto);
        await _repository.UpdateAsync(entity, cancellationToken);
        return Unit.Value;
    }
}
