using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Team;

public record DeleteTeamCommand(TeamDto TeamDto) : IRequest<ResponseTemplate>;

public class DeleteTeamHandler : IRequestHandler<DeleteTeamCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;
    
    public DeleteTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(request.TeamDto);
        var validate = Validate(entity);

        if (validate.Item1)
            return new ResponseTemplate(HttpStatusCode.BadRequest, validate.Item2);

        try
        {
            await _repository.InsertAsync(entity, cancellationToken);
        }
        catch (Exception e)
        {
            validate.Item2.Add(e.InnerException?.Message ?? new string("Unknown error"));
        }

        var code = validate.Item2.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        return  new ResponseTemplate(code, validate.Item2);
    }
    
    private static Tuple<bool, List<string>> Validate(domain.Entities.Team entity)
    {
        var errors = new List<string>();
       
        var isErr = errors.Count > 0;
        return Tuple.Create(isErr, errors);
    }
}