﻿using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSubmitApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handle = profession switch
        {
            UserProfession.Entrepreneur => HandleSubmitEntrepreneurApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Investor => HandleSubmitInvestmentApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.ProjectManager => HandleSubmitProjectManagerApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Representative => HandleSubmitRepresentativeApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query application submit: {callbackQuery.Data}", callbackQuery.Data); }

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitRepresentativeApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nVakillik qilish uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateRepresentativeIsSubmittedCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
        Thread.Sleep(1000);
    }

    private async Task HandleSubmitProjectManagerApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nLoyiha boshqarish uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateProjectManagerIsSubmittedCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
        Thread.Sleep(1000);
    }

    private async Task HandleSubmitEntrepreneurApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nInvestitsiya jalb qilish bo'yicha murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateEntrepreneurIsSubmittedCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
        Thread.Sleep(1000);
    }

    private async Task HandleSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: "Tabriklaymiz!\nInvestorlik uchun murojaatingiz qabul qilindi va tez orada siz bilan bog'lanamiz!",
            cancellationToken: cancellationToken);

        await mediator.Send(new UpdateInvestorIsSubmittedCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
        Thread.Sleep(1000);
    }
}