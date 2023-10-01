using domain.Dto;
using domain.Entities;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.TeamStats;

public record GetAllTeamStatsQuery() : IRequest<ICollection<TeamStatsDto>>;

public class GetAllTeamStatsHandler : IRequestHandler<GetAllTeamStatsQuery,ICollection<TeamStatsDto>>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetAllTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<TeamStatsDto>> Handle(GetAllTeamStatsQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map(data);
    }
}
