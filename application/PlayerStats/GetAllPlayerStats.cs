using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.PlayerStats;

public record GetAllPlayerStatsQuery : IRequest<ICollection<PlayerStatsDto>>;

public class GetAllPlayerStatsHandler : IRequestHandler<GetAllPlayerStatsQuery, ICollection<PlayerStatsDto>>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public GetAllPlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<PlayerStatsDto>> Handle(GetAllPlayerStatsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map(await _repository.GetAllAsync(cancellationToken));
    }
}