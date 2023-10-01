using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Player;

public record UpdatePlayerCommand(PlayerDto PlayerDto) : IRequest<Unit>;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Unit>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public UpdatePlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(_mapper.Map(request.PlayerDto), cancellationToken);
        return Unit.Value;
    }
}