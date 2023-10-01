using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Player;

public record GetByIdPlayerQuery(Guid Id) : IRequest<PlayerDto>;

public class GetByIdPlayerHandler : IRequestHandler<GetByIdPlayerQuery, PlayerDto>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdPlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(GetByIdPlayerQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (data is not null)
            return _mapper.Map(data);
        
        throw new Exception("");//TODO
    }
}