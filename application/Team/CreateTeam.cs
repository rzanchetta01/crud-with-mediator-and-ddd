using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Team;

public record CreateTeamCommand(TeamDto TeamDto) : IRequest<Unit>;

public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, Unit>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    
    public CreateTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        await _repository.InsertAsync(_mapper.Map(request.TeamDto), cancellationToken);
        return Unit.Value;
    }
}