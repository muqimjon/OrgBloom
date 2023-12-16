﻿using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Domain.Entities.Representation;

namespace OrgBloom.Application.Representatives.Queries.GetRepresentatives;

public record GetRepresentativeByUserIdQuery : IRequest<RepresentativeResultDto>
{
    public GetRepresentativeByUserIdQuery(long userId) { UserId = userId; }
    public long UserId { get; set; }
}

public class GetRepresentativeByUserIdQueryHendler(IRepository<Representative> repository, IMapper mapper) : IRequestHandler<GetRepresentativeByUserIdQuery, RepresentativeResultDto>
{
    public async Task<RepresentativeResultDto> Handle(GetRepresentativeByUserIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<RepresentativeResultDto>(await repository.SelectAsync(i => i.UserId.Equals(request.UserId), includes: ["User"]));
}
