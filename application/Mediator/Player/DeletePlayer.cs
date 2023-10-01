using System.Net;
using domain.Dto;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Player;

public record DeletePlayerCommand(PlayerDto Dto) : IRequest<ResponseTemplate>;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public DeletePlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var entity = _mapper.Map(request.Dto);

        try
        {
            await _repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception e)
        {
            errors.Add(e.InnerException?.Message ?? new string("Unknown error"));
        }

        var code = errors.Count > 0 ? HttpStatusCode.BadGateway : HttpStatusCode.OK;
        return new ResponseTemplate(code, errors);
    }
     
}