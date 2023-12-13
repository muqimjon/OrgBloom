﻿using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.DTOs;

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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectFieldApplication), cancellationToken);
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

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectFieldApplication), cancellationToken);
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
            $"";

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: text,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectFieldApplication), cancellationToken);
    }

    private async Task SendGreeting(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);


        var isUserNew = await mediator.Send(new IsUserNewQuery(user.Id), cancellationToken);
        if (isUserNew)
        {
            var keyboard = new InlineKeyboardMarkup(new[] {
                InlineKeyboardButton.WithCallbackData("🇺🇿", "buttonLanguageUz"),
                InlineKeyboardButton.WithCallbackData("🇬🇧", "buttonLanguageEn"),
                InlineKeyboardButton.WithCallbackData("🇷🇺", "buttonLanguageRu")
            });

            string text = $"Assalomu alaykum {user.FirstName} {user.LastName}!\nO'zingiz uchun qulay tilni tanlang:";
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: text, // localizer[message.Text!, user.FirstName],
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

            await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectLanguage), cancellationToken);
        }
        else await SendMainMenuAsync(botClient, message, cancellationToken);
    }

    public async Task SendMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton("Ariza topshirish") },
            new[] { new KeyboardButton("Contact"), new KeyboardButton("Fikr bildirish") },
            new[] { new KeyboardButton("Sozlamalar"), new KeyboardButton("Information"), }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Menyuni tanlang:",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectMainMenu), cancellationToken);
    }
}
