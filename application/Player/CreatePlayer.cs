using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Player;


public record CreatePlayerCommand(PlayerDto PlayerDto) : IRequest<Unit>;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, Unit>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;
    
    public CreatePlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        await _repository.InsertAsync(_mapper.Map(request.PlayerDto), cancellationToken);
        return Unit.Value;
    }
}