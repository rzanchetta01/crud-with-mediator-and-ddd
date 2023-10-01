using System.Net;
using domain.Entities;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.TeamStats;

public record GetByIdTeamStatsQuery(Guid TeamId) : IRequest<ResponseTemplate>;


public class GetByIdTeamStatsHandler : IRequestHandler<GetByIdTeamStatsQuery, ResponseTemplate>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResponseTemplate> Handle(GetByIdTeamStatsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetByIdAsync(request.TeamId, cancellationToken);
            var code = data is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;

            return new ResponseTemplate(code, data);
        }
        catch (Exception e)
        {
            return new ResponseTemplate(HttpStatusCode.BadRequest, e.InnerException?.Message ?? new string("Unknown error"));
        }
    }
}
