﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Commons.Interfaces;

namespace OrgBloom.Application.Investors.Commands.CreateInvestors;

public record CreateInvestorCommand : IRequest<int>
{
    public CreateInvestorCommand(CreateInvestorCommand command)
    {
        UserId = command.UserId;
        Sector = command.Sector;
        InvestmentAmount = command.InvestmentAmount;
    }

    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public long UserId { get; set; }
}

public class CreateInvestorCommandHandler(IRepository<Investor> repository, IMapper mapper) : IRequestHandler<CreateInvestorCommand, int>
{
    public async Task<int> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.UserId == request.UserId);
        if (entity is not null)
            throw new();

        await repository.InsertAsync(mapper.Map<Investor>(request));
        return await repository.SaveAsync();
    }
}