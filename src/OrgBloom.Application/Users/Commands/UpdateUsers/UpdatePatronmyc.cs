﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdatePatronmycCommand : IRequest<int>
{
    public UpdatePatronmycCommand(UpdatePatronmycCommand command)
    {
        Id = command.Id;
        Patronmyc = command.Patronmyc;
    }

    public long Id { get; set; }
    public string Patronmyc { get; set; } = string.Empty;
}

public class UpdatePatronmycCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdatePatronmycCommand, int>
{
    public async Task<int> Handle(UpdatePatronmycCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | update ptronmyc");

        mapper.Map(request, entity);
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}