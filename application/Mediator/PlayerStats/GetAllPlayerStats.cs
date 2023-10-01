using System.Net;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.PlayerStats;

public record GetAllPlayerStatsQuery : IRequest<ResponseTemplate>;

public class GetAllPlayerStatsHandler : IRequestHandler<GetAllPlayerStatsQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetAllPlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetAllPlayerStatsQuery request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map(await _repository.GetAllAsync(cancellationToken));
        
        var code  = data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        return new ResponseTemplate(code, data);
    }
}