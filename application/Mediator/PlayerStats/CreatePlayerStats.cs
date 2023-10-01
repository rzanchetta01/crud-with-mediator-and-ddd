using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.PlayerStats;

public record CreatePlayerStatsCommand(PlayerStatsDto TeamDto) : IRequest<ResponseTemplate>;

public class CreatePlayerStatsHandler : IRequestHandler<CreatePlayerStatsCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.PlayerStat> _repository;
    private readonly MapperlyMapper _mapper;

    public CreatePlayerStatsHandler(IRepository<domain.Entities.PlayerStat> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(CreatePlayerStatsCommand request, CancellationToken cancellationToken)
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
    
    private static Tuple<bool, List<string>> Validate(domain.Entities.PlayerStat entity)
    {
        var errors = new List<string>();
        if(entity.Points < 0)
            errors.Add("Points need to be greater than or equals 0");

        if(entity.Rebounds < 0)
            errors.Add("Rebounds need to be greater than or equals 0");

        if(entity.Assists < 0)
            errors.Add("Assists need to be greater than or equals 0");

        if(entity.Turnover < 0)
            errors.Add("Turnover need to be greater than or equals 0");
        
        var isErr = errors.Count > 0;
        return Tuple.Create(isErr, errors);
    }
}