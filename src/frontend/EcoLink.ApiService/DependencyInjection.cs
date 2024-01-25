﻿using EcoLink.ApiService.Services;
using Microsoft.Extensions.Configuration;
using EcoLink.ApiService.Services.Investment;
using EcoLink.ApiService.Interfaces.Investment;
using Microsoft.Extensions.DependencyInjection;
using EcoLink.ApiService.Services.Representation;
using EcoLink.ApiService.Interfaces.Representation;
using EcoLink.ApiService.Services.Entrepreneurship;
using EcoLink.ApiService.Services.ProjectManagement;
using EcoLink.ApiService.Interfaces.Entrepreneurship;
using EcoLink.ApiService.Interfaces.ProjectManagement;

namespace EcoLink.ApiService;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var baseLink = configuration["BaseLink"];

        services.AddHttpClient<IUserService, UserService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/Users/"); });

        services.AddHttpClient<IInvestmentAppService, InvestmentAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/InvestmentApps/"); });

        services.AddHttpClient<IRepresentationAppService, RepresentationAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/RepresentationApps/"); });

        services.AddHttpClient<IEntrepreneurshipAppService, EntrepreneurshipAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/EntrepreneurshipApps/"); });

        services.AddHttpClient<IProjectManagementAppService, ProjectManagementAppService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/ProjectManagementApps/"); });

        services.AddHttpClient<IInvestmentService, InvestmentService>(client => 
            { client.BaseAddress = new Uri($"{baseLink}api/Investments/"); });

        services.AddHttpClient<IRepresentationService, RepresentationService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/Representations/"); });

        services.AddHttpClient<IEntrepreneurshipService, EntrepreneurshipService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/Entrepreneurships/"); });

        services.AddHttpClient<IProjectManagementService, ProjectManagementService>(client =>
            { client.BaseAddress = new Uri($"{baseLink}api/ProjectManagements/"); });

        return services;
    }
}