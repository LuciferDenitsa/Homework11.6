
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Homework11._6.Controllers;

namespace Homework11._6
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;

        private InlineKeyController _inlineKeyController;
        private TextMessageController _textMessageController;
        private DefaultMessageController _defaultMessageController;

        public Bot(
          ITelegramBotClient telegramClient,
          InlineKeyController inlineKeyController,
          TextMessageController textMessageController,
          DefaultMessageController defaultMessageController)
        {
            _telegramClient = telegramClient;
            _inlineKeyController = inlineKeyController;
            _textMessageController = textMessageController;
            _defaultMessageController = defaultMessageController;
        }




        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }


            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидание 10 сек перед повторной попыткой");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}