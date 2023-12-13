﻿using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;

namespace OrgBloom.Application.Investors.Commands.DeleteInvestors;

public record DeleteInvestorCommand : IRequest<bool>
{
    public DeleteInvestorCommand(DeleteInvestorCommand command) { Id = command.Id; }
    public int Id { get; set; }
}

public class DeleteInvestorCommandHandler(IRepository<Investor> repository) : IRequestHandler<DeleteInvestorCommand, bool>
{
    public async Task<bool> Handle(DeleteInvestorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"Investor is not found with UserId: {request.Id} by User UserId: {request.Id} | Update Investor");

        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}