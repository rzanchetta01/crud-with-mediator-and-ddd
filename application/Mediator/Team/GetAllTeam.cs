using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Team;

public record GetAllTeamQuery : IRequest<ResponseTemplate>;

public class GetAllTeamHandler : IRequestHandler<GetAllTeamQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    
    
    public GetAllTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map(await _repository.GetAllAsync(cancellationToken));
        
        var code  = data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        return new ResponseTemplate(code, data);
    }
}