﻿using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Investors.DTOs;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task SendApplyQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("Investitsiya jalb qilish") },
            new[] { new KeyboardButton("Investorlik qilish") },
            new[] { new KeyboardButton("Vakil bo'lish") },
            new[] { new KeyboardButton("Loyiha boshqarish") }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Nima maqsadda ariza topshirmoqchisiz?",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectProfession), cancellationToken);
    }

    private async Task SendAlreadyExistApplicationAsync(InvestorResultDto dto, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("Qayta yuborish") },
            new[] { new KeyboardButton("Ortga") }
        })
        { ResizeKeyboard = true };

        var text = $"Sizda Tastiqlangan murojaat mavjud\n" +
            $"Ism: {dto.User.FirstName}\n" +
            $"Familiya: {dto.User.LastName}\n" +
            $"Otasining ismi: {dto.User.Patronomyc}\n" +
            $"Yoshi: {(DateTime.UtcNow - dto.User.DateOfBirth).ToString()!.Split().First()}";

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterFirstName), cancellationToken);
    }

    private async Task SendRequestForFirstNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(user.FirstName) }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Ismingizni kiriting: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterFirstName), cancellationToken);
    }

    private async Task SendRequestForLastNameAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(user.LastName) }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Familiyangizni kiriting: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterLastName), cancellationToken);
    }

    private async Task SendRequestForPatronomycAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var exist = await mediator.Send(new GetUserByIdQuery() { Id = user.Id }, cancellationToken);
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Otangizning ismi: ",
            cancellationToken: cancellationToken,
            replyMarkup: string.IsNullOrEmpty(exist.Patronomyc) ?
                new ReplyKeyboardRemove() :
                new ReplyKeyboardMarkup(new[]
                {
                    new[] { new KeyboardButton(exist.Patronomyc) }
                })
                { ResizeKeyboard = true });

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterPatronomyc), cancellationToken);
    }

    private async Task SendRequestForDateOfBirthAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var exist = await mediator.Send(new GetUserByIdQuery() { Id = user.Id }, cancellationToken);
        var formattedDate = exist.DateOfBirth.ToString()!.Split().First();
        var isWithinRange = exist.DateOfBirth >= new DateTimeOffset(1950, 1, 1, 0, 0, 0, TimeSpan.Zero)
                            && exist.DateOfBirth <= new DateTimeOffset(2005, 12, 31, 23, 59, 59, TimeSpan.Zero);

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Tug'ilgan sana: (kk.oo.yyyy)",
            cancellationToken: cancellationToken,
            replyMarkup: isWithinRange ?
                new ReplyKeyboardMarkup(new[]
                {
                    new[] { new KeyboardButton(formattedDate) }
                }) { ResizeKeyboard = true } :
                new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDateOfBirth), cancellationToken);
    }

    private async Task SendRequestForDegreeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("O'rta"), new KeyboardButton("O'rta maxsusus") },
            new[] { new KeyboardButton("Oliy"), new KeyboardButton("Magistr") }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Ma'lumotingiz: ",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterDegree), cancellationToken);
    }

    private async Task SendRequestForSectorAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qaysi sohada investorlik qilmoqchisiz?",
            cancellationToken: cancellationToken,
            replyMarkup: new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterSector), cancellationToken);
    }

    private async Task SendRequestForInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Qancha miqdorda investitsiya kiritmoqchisiz?\nDollardayozing:",
            cancellationToken: cancellationToken,
            replyMarkup: new ReplyKeyboardRemove());

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForEnterInvestmentAmount), cancellationToken);
    }

    private async Task SendForSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var investor = await mediator.Send(new GetInvestorByUserIdQuery() { UserId = user.Id }, cancellationToken);

        var keyboard = new InlineKeyboardMarkup(new[] {
                new[] { InlineKeyboardButton.WithCallbackData("Tasdiqlash", "submit") },
                new[] { InlineKeyboardButton.WithCallbackData("E'tiborsiz qoldrish", "cancel") }
            });

        var text = $"Ma'lumotlarni tasdiqlang:\n" +
            $"Ism: {investor.User.FirstName}\n" +
            $"Familiya: {investor.User.LastName}\n" +
            $"Otasining ismi: {investor.User.Patronomyc}\n" +
            $"Yoshi: {(DateTime.UtcNow - investor.User.DateOfBirth).ToString()!.Split().First()}";

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSubmitApplication), cancellationToken);
    }
}