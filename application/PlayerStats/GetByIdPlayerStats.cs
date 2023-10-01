using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.PlayerStats;

public record GetByIdPlayerStatsQuery(Guid Id) : IRequest<PlayerStatsDto>;

public class GetByIdPlayerStatsHandler : IRequestHandler<GetByIdPlayerStatsQuery, PlayerStatsDto>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdPlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayerStatsDto> Handle(GetByIdPlayerStatsQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (data is not null)
            return _mapper.Map(data);

        throw new Exception(""); //TODO
    }
}