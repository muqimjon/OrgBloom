﻿using EcoLink.ApiService.Models.Users;

namespace EcoLink.ApiService.Models.Investment;

public class InvestmentAppDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string InvestmentAmount { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOld { get; set; }

    public long UserId { get; set; }
    public UserDto User { get; set; } = default!;
}
