using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramLib.Interfaces;
using TelegramLib.Models;

namespace TelegramLib;

public class Mytelbot
{
    public const string Token = "2031108528:AAE9qpNIRCbpF23IufRJTO-0D2h7IyIf0gg";
    private readonly IRepository<UserModel> _repository;
    public UserModel Mytelbol = new();

    private TelegramBotClient? _client;
    private CancellationTokenSource cts = new();

    public Mytelbot(IRepository<UserModel> repository)
    {
        Init().Result
            .StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions { AllowedUpdates = { } },
                cts.Token);

        _repository = repository;
    }

    public async Task<TelegramBotClient> Init()
    {
        _client = new TelegramBotClient(Token);
        var temp = await _client.GetMeAsync();

        Mytelbol.Id = temp.Id;
        Mytelbol.FirstName = temp.FirstName;

        return _client;
    }
    public async Task SendMessageAsync(string message, IEnumerable<UserModel> recievers)
    {
        if (_client == null) return;

        foreach (var reciever in recievers)
        {
            var msg = $"Dear {reciever.FirstName}, {message}";
            await _client.SendTextMessageAsync(chatId: reciever.Id,
                text: msg);
        }
    }

    protected static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
    protected async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handler = update.Type switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
            UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!),
            //UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
            //UpdateType.InlineQuery => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
            //UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult!),
            _ => UnknownUpdateHandlerAsync(botClient, update)
        };

        try
        {
            await handler;
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(botClient, exception, cancellationToken);
        }
    }
    protected Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
    {
        Debug.WriteLine($"Receive message type: {message.Type}\t {message.Text}");
        if (message.Text == "/start")
        {
            UserModel new_user = new();
            if (message.From is User user) 
            {
                new_user.Id = user.Id;
                new_user.FirstName = user.FirstName;

                return _repository.CreateAsync(new_user);
            }
        }
        return Task.CompletedTask;
    }
    protected Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
    {
        Debug.WriteLine($"Unknown update type: {update.Type}");
        return Task.CompletedTask;
    }

}
