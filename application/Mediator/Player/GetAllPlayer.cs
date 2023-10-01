using System.Net;
using domain.Mapping;
using domain.Network;
using FluentValidation;
using infrastructure;
using MediatR;

namespace application.Mediator.Player;

public record GetAllPlayerQuery : IRequest<ResponseTemplate>;

public class GetAllPlayerHandler : IRequestHandler<GetAllPlayerQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;
    
    public GetAllPlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetAllPlayerQuery request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map(await _repository.GetAllAsync(cancellationToken));
        
        var code  = data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
        return new ResponseTemplate(code, data);
    }
}