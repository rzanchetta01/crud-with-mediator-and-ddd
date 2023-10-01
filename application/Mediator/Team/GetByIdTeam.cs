using System.Net;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Team;

public record GetByIdTeamQuery(Guid TeamId) : IRequest<ResponseTemplate>;

public class GetByIdTeamHandler : IRequestHandler<GetByIdTeamQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    

    public GetByIdTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetByIdTeamQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetByIdAsync(request.TeamId, cancellationToken);
            var code = data is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;

            return new ResponseTemplate(code, data);
        }
        catch (Exception e)
        {
            return new ResponseTemplate(HttpStatusCode.BadRequest, e.InnerException?.Message ?? new string("Unknown error"));
        }
    }
}