using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Team;

public record DeleteTeamCommand(TeamDto TeamDto) : IRequest<Unit>;

public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, Unit>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    
    public DeleteTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(_mapper.Map(request.TeamDto), cancellationToken);
        return Unit.Value;
    }
}