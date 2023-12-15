﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;

public record CreateEntrepreneurWithReturnCommand : IRequest<EntrepreneurResultDto>
{
    public CreateEntrepreneurWithReturnCommand(CreateEntrepreneurWithReturnCommand command)
    {
        UserId = command.UserId;
        Project = command.Project;
        HelpType = command.HelpType;
        AssetsInvested = command.AssetsInvested;
        RequiredFunding = command.RequiredFunding;
    }

    public string Project { get; set; } = string.Empty;
    public string HelpType { get; set; } = string.Empty;
    public string RequiredFunding { get; set; } = string.Empty;
    public string AssetsInvested { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateEntrepreneurWithReturnCommandHandler(IRepository<Entrepreneur> repository, IMapper mapper) : IRequestHandler<CreateEntrepreneurWithReturnCommand, EntrepreneurResultDto>
{
    public async Task<EntrepreneurResultDto> Handle(CreateEntrepreneurWithReturnCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new AlreadyExistException($"Entrepreneur is already exist create Entrepreneur by user id {request.UserId}");

        entity = mapper.Map<Entrepreneur>(request);
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<EntrepreneurResultDto>(entity);
    }
}