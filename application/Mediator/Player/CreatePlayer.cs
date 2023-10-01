using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Player;


public record CreatePlayerCommand(PlayerDto PlayerDto) : IRequest<ResponseTemplate>;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;
    
    public CreatePlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map(request.PlayerDto);
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

    private static Tuple<bool, List<string>> Validate(domain.Entities.Player entity)
    {
        var errors = new List<string>();
        if(entity.Age < 15)
            errors.Add("Age must be greater than 15\n");

        if(entity.Salary < 0)
            errors.Add("Salary must be grater than 0\n");

        var isErr = errors.Count > 0;
        return Tuple.Create(isErr, errors);
    }
}