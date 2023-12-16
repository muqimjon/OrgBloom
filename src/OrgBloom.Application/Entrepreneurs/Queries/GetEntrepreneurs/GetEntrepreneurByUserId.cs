﻿using AutoMapper;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Domain.Entities.Entrepreneurship;

namespace OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;

public record GetEntrepreneurByUserIdQuery : IRequest<EntrepreneurResultDto>
{
    public GetEntrepreneurByUserIdQuery(long id) { UserId = id; }
    public long UserId { get; set; }
}

public class GetEntrepreneurByUserIdQueryHendler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<GetEntrepreneurByUserIdQuery, EntrepreneurResultDto>
{
    public async Task<EntrepreneurResultDto> Handle(GetEntrepreneurByUserIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<EntrepreneurResultDto>(await repository.SelectAsync(i => i.UserId.Equals(request.UserId)));
}
