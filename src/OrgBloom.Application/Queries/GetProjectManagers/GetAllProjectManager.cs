﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Interfaces;
using OrgBloom.Application.DTOs.ProjectManagers;

namespace OrgBloom.Application.Queries.GetProjectManagers;

public record GetAllProjectManagersQuery : IRequest<IEnumerable<ProjectManagerResultDto>> { }

public class GetAllProjectManagersQueryHandler(IRepository<ProjectManager> repository, IMapper mapper) : IRequestHandler<GetAllProjectManagersQuery, IEnumerable<ProjectManagerResultDto>>
{
    public async Task<IEnumerable<ProjectManagerResultDto>> Handle(GetAllProjectManagersQuery request, CancellationToken cancellationToken)
    => await Task.Run(() => mapper.Map<IEnumerable<ProjectManagerResultDto>>(repository.SelectAll().ToList()));
}
