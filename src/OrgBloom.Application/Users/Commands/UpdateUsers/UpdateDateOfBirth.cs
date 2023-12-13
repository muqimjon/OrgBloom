﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateDateOfBirthCommand : IRequest<int>
{
    public UpdateDateOfBirthCommand(UpdateDateOfBirthCommand command)
    {
        Id = command.Id;
        DateOfBirth = command.DateOfBirth;
    }

    public long Id { get; set; }
    public string DateOfBirth { get; set; } = string.Empty;
}

public class UpdateDateOfBirthCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateDateOfBirthCommand, int>
{
    public async Task<int> Handle(UpdateDateOfBirthCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update date of birth");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}