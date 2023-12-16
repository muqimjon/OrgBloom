﻿using AutoMapper;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.DTOs;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Application.Commons.Exceptions;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Application.Users.Commands.CreateUsers;

public record class CreateUserWithReturnTgResultCommand : IRequest<UserTelegramResultDto>
{
    public CreateUserWithReturnTgResultCommand(CreateUserWithReturnTgResultCommand command)
    {
        Phone = command.Phone;
        Email = command.Email;
        IsBot = command.IsBot;
        Degree = command.Degree;
        ChatId = command.ChatId;
        Address = command.Address;
        Username = command.Username;
        LastName = command.LastName;
        Languages = command.Languages;
        FirstName = command.FirstName;
        TelegramId = command.TelegramId;
        Patronomyc = command.Patronomyc;
        Experience = command.Experience;
        Profession = command.Profession;
        DateOfBirth = command.DateOfBirth;
        LanguageCode = command.LanguageCode;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronomyc { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserProfession Profession { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
}

public class CreateUserWithReturnTgResultCommandHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<CreateUserWithReturnTgResultCommand, UserTelegramResultDto>
{
    public async Task<UserTelegramResultDto> Handle(CreateUserWithReturnTgResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.TelegramId == request.TelegramId);
        if (entity is not null)
            throw new AlreadyExistException($"User Already exist user command create with telegram id: {request.TelegramId} | create user with return tg result");

        var user = mapper.Map<User>(request);
        await repository.InsertAsync(user);
        await repository.SaveAsync();

        return mapper.Map<UserTelegramResultDto>(user);
    }
}