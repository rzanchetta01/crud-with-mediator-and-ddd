using domain.Dto;
using domain.Mapping;
using infrastructure;
using MediatR;

namespace application.Team;

public record GetAllTeamQuery : IRequest<ICollection<TeamDto>>;

public class GetAllTeamHandler : IRequestHandler<GetAllTeamQuery, ICollection<TeamDto>>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    
    
    public GetAllTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<TeamDto>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map(data);
    }
}