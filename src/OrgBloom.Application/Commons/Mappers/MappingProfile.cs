﻿using AutoMapper;
using OrgBloom.Domain.Entities;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Entrepreneurs.DTOs;
using OrgBloom.Application.ProjectManagers.DTOs;
using OrgBloom.Application.Representatives.DTOs;
using OrgBloom.Application.Users.Commands.CreateUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

namespace OrgBloom.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Investor
        CreateMap<Investor, InvestorResultDto>();

        CreateMap<UpdateInvestorCommand, Investor>();
        CreateMap<UpdateInvestorInvestmentAmountCommand, Investor>();
        CreateMap<UpdateInvestorIsSubmittedCommand, Investor>();
        CreateMap<UpdateInvestorSectorCommand, Investor>();

        CreateMap<CreateInvestorCommand, Investor>();
        CreateMap<CreateInvestorWithReturnCommand, Investor>();


        // Project Manager
        CreateMap<ProjectManager, ProjectManagerResultDto>();

        CreateMap<UpdateProjectManagerCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerAddressCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerAreaCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerExpectationCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerExperienceCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerIsSubmittedCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerLanguagesCommand, ProjectManager>();
        CreateMap<UpdateProjectManagerPurposeCommand, ProjectManager>();

        CreateMap<CreateProjectManagerCommand, ProjectManager>();


        // Entrepreneur
        CreateMap<Entrepreneur, EntrepreneurResultDto>();

        CreateMap<UpdateEntrepreneurCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurExperienceCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurHelpTypeCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurAssetsInvestedCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurInvestmentAmountCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurIsSubmittedCommand, Entrepreneur>();
        CreateMap<UpdateEntrepreneurProjectCommand, Entrepreneur>();

        CreateMap<CreateEntrepreneurCommand, Entrepreneur>();


        // Representative
        CreateMap<Representative, RepresentativeResultDto>();

        CreateMap<UpdateRepresentativeCommand, Representative>();
        CreateMap<UpdateRepresentativeAddressCommand, Representative>();
        CreateMap<UpdateRepresentativeAreaCommand, Representative>();
        CreateMap<UpdateRepresentativeExpectationCommand, Representative>();
        CreateMap<UpdateRepresentativeExperienceCommand, Representative>();
        CreateMap<UpdateRepresentativeIsSubmittedCommand, Representative>();
        CreateMap<UpdateRepresentativeLanguagesCommand, Representative>();
        CreateMap<UpdateRepresentativePurposeCommand, Representative>();

        CreateMap<CreateRepresentativeCommand, Representative>();


        // User
        CreateMap<User, UserApplyResultDto>();
        CreateMap<User, UserTelegramResultDto>();
        CreateMap<User, UserApplyResultDto>();

        CreateMap<UpdateUserCommand, User>();
        CreateMap<UpdateDateOfBirthCommand, User>();
        CreateMap<UpdateDegreeCommand, User>();
        CreateMap<UpdateEmailCommand, User>();
        CreateMap<UpdateFirstNameCommand, User>();
        CreateMap<UpdateLastNameCommand, User>();
        CreateMap<UpdatePatronomycCommand, User>();
        CreateMap<UpdatePhoneCommand, User>();
        CreateMap<UpdateStateCommand, User>();
        CreateMap<UpdateLanguageCodeCommand, User>();
        CreateMap<UpdateProfessionCommand, User>();

        CreateMap<CreateUserCommand, User>();
        CreateMap<CreateUserWithReturnTgResultCommand, User>();
    }
}