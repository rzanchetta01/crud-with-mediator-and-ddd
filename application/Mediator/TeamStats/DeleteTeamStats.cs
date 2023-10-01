using System.Net;
using domain.Dto;
using domain.Entities;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.TeamStats;

public record DeleteTeamStatsCommand(TeamStatsDto Dto) : IRequest<ResponseTemplate>;

public class DeleteTeamStatsHandler : IRequestHandler<DeleteTeamStatsCommand, ResponseTemplate>
{
    private readonly IRepository<TeamStat> _repository;
    private readonly MapperlyMapper _mapper;

    public DeleteTeamStatsHandler(IRepository<TeamStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(DeleteTeamStatsCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(request.Dto);
        var validate = Validate(entity);

        if (validate.Item1)
            return new ResponseTemplate(HttpStatusCode.BadRequest, validate.Item2);

        try
        {
            await _repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception e)
        {
            validate.Item2.Add(e.InnerException?.Message ?? new string("Unknown error"));
        }

        var code = validate.Item2.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        return  new ResponseTemplate(code, validate.Item2);
    }

    private static Tuple<bool, List<string>> Validate(domain.Entities.TeamStat entity)
    {
        var errors = new List<string>();
        if(entity.Title < 0)
            errors.Add("Title count must be greater than 0\n");

        var isErr = errors.Count > 0;
        return Tuple.Create(isErr, errors);
    }
}
