using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Player;

public record GetAllPlayerQuery : IRequest<ICollection<PlayerDto>>;

public class GetAllPlayerHandler : IRequestHandler<GetAllPlayerQuery, ICollection<PlayerDto>>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public GetAllPlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<PlayerDto>> Handle(GetAllPlayerQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map(await _repository.GetAllAsync(cancellationToken));
    }
}