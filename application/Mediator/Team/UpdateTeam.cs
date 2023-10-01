using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Team;


public record UpdateTeamCommand(TeamDto Dto) : IRequest<ResponseTemplate>;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Team> _repository;
    private readonly MapperlyMapper _mapper;

    public UpdateTeamHandler(IRepository<domain.Entities.Team> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(request.Dto);
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