using System.Net;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.PlayerStats;

public record GetByIdPlayerStatsQuery(Guid Id) : IRequest<ResponseTemplate>;

public class GetByIdPlayerStatsHandler : IRequestHandler<GetByIdPlayerStatsQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdPlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetByIdPlayerStatsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var code = data is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;

            return new ResponseTemplate(code, data);
        }
        catch (Exception e)
        {
            return new ResponseTemplate(HttpStatusCode.BadRequest, e.InnerException?.Message ?? new string("Unknown error"));
        }
    }
}