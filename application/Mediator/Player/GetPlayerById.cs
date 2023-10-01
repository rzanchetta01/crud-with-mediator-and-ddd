﻿using System.Net;
using domain.Mapping;
using domain.Network;
using infrastructure;
using MediatR;

namespace application.Mediator.Player;

public record GetByIdPlayerQuery(Guid Id) : IRequest<ResponseTemplate>;

public class GetByIdPlayerHandler : IRequestHandler<GetByIdPlayerQuery, ResponseTemplate>
{
    private readonly IRepository<domain.Entities.Player> _repository;
    private readonly MapperlyMapper _mapper;

    public GetByIdPlayerHandler(IRepository<domain.Entities.Player> repository, MapperlyMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTemplate> Handle(GetByIdPlayerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var code = data is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;

            return new ResponseTemplate(code, data);
        }
        catch (Exception e)
        {
            return new ResponseTemplate(HttpStatusCode.BadRequest, e.InnerException?.Message ?? new string("Unknown error"));
        }
    }
}