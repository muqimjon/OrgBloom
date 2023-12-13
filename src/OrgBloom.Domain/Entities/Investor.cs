﻿namespace OrgBloom.Domain.Entities;

public class Investor : Auditable
{
    public string? Sector { get; set; } 
    public decimal? InvestmentAmount { get; set; }
    public bool IsSubmitted { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
