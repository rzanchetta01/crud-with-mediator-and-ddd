using System.Net;
using domain.Dto;
using domain.Entities;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.TeamStats;

public record GetAllTeamStatsQuery() : IRequest<ResponseTemplate>;

public class GetAllTeamStatsHandler : IRequestHandler<GetAllTeamStatsQuery,ResponseTemplate>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetAllTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetAllTeamStatsQuery request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map(await _repository.GetAllAsync(cancellationToken));
        
        var code  = data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        return new ResponseTemplate(code, data);
    }
}
