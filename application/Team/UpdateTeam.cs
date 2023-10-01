using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;
namespace application.Team;


public record UpdateTeamCommand(TeamDto Dto) : IRequest<Unit>;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, Unit>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;

    public UpdateTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(_mapper.Map(request.Dto), cancellationToken);
        return Unit.Value;
    }
}