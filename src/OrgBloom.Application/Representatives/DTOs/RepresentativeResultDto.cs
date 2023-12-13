﻿using OrgBloom.Application.Users.DTOs;

namespace OrgBloom.Application.Representatives.DTOs;

public class RepresentativeResultDto
{
    public long Id { get; set; }
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Expectation { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public UserApplyResultDto User { get; set; } = default!;
    public bool IsSubmitted { get; set; }
}
