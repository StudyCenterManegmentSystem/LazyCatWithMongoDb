using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Application.Commens.Helpers
{
    public static class LoggerBot
    {
        private static readonly long ChatId = -1002102918854; // Your channel ID

        public static async Task Log(string message, LogType logType)
        {
            var botClient = new TelegramBotClient("5633343007:AAFn8VK7oMUgF1GWmO6fsGS6bjCnmD1C4Zg");
            var smile = logType switch
            {
                LogType.Info => "✅",
                LogType.Warning => "⚠️",
                LogType.Error => "❌",
                _ => "❓"
            };

            await botClient.SendTextMessageAsync(
                chatId: ChatId,
                text: $@"
[{smile} {logType}] : {DateTime.Now}

{message}",
                parseMode: ParseMode.Markdown,
                disableNotification: true);
        }
    }

    public enum LogType
    {
        Info,
        Warning,
        Error
    }
}
