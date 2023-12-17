﻿using AutoMapper;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;
using OrgBloom.Application.Commons.Helpers;

namespace OrgBloom.Application.Users.Commands.UpdateUsers;

public record UpdateAddressCommand : IRequest<int>
{
    public UpdateAddressCommand(UpdateAddressCommand command)
    {
        Id = command.Id;    
        Address = command.Address;
    }

    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
}

public class UpdateAddressCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<UpdateAddressCommand, int>
{
    public async Task<int> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | Address update");

        mapper.Map(request, entity);
        entity.UpdatedAt = TimeHelper.GetDateTime();
        repository.Update(entity);
        return await repository.SaveAsync();
    }
}