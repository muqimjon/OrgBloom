﻿namespace MedX.ApiService.Services;

public class InvestmentService(HttpClient client) : IInvestmentService
{
    public async Task<InvestmentDto> AddAsync(InvestmentDto dto, CancellationToken cancellationToken)
    {
        using var content = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PostAsync("create", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<InvestmentDto>(cancellationToken: cancellationToken))!;
    }

    public async Task<int> UpdateAsync(InvestmentDto dto, CancellationToken cancellationToken)
    {
        using var multipartFormContent = ConvertHelper.ConvertToStringContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken))!;
    }

    public async Task<InvestmentDto> GetAsync(long id, CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync($"get/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<InvestmentDto>(cancellationToken: cancellationToken))!;
    }
}
