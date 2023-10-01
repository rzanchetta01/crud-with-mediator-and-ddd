using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Player;

public record DeletePlayerCommand(PlayerDto PlayerDto) : IRequest<Unit>;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Unit>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public DeletePlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(_mapper.Map(request.PlayerDto), cancellationToken);
        return Unit.Value;
    }
}