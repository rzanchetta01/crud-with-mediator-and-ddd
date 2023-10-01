using domain.Dto;
using domain.Entities;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.TeamStats;

public record GetByIdTeamStatsQuery(Guid TeamId) : IRequest<TeamStatsDto>;


public class GetByIdTeamStatsHandler : IRequestHandler<GetByIdTeamStatsQuery, TeamStatsDto>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<TeamStatsDto> Handle(GetByIdTeamStatsQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.TeamId, cancellationToken);
        
        if(data is not null)
            return _mapper.Map(data);

        throw new Exception("");//TODO
    }
}
