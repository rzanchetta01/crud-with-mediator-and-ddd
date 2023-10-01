using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Team;

public record GetByIdTeamQuery(Guid TeamId) : IRequest<TeamDto>;

public class GetByIdTeamHandler : IRequestHandler<GetByIdTeamQuery, TeamDto>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    

    public GetByIdTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TeamDto> Handle(GetByIdTeamQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.TeamId, cancellationToken);
        
        if(data is not null)
            return _mapper.Map(data);

        throw new Exception("");//TODO
    }
}