using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuoteOfTheDay;
using QuoteOfTheDay.Context;
using QuoteOfTheDay.Context.Repository;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

public class QotdTask : IInvocable
{
    private readonly BotConfiguration config;
    private readonly IRepository<Chat> db;

    public QotdTask(IOptions<BotConfiguration> _config, IRepository<Chat> _db)
    {
        config = _config.Value;
        db = _db;
    }

    public async Task Invoke()
    {
        //Retrive the quote of the day
        var httpClient = new HttpClient();
        var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));
        var quote = JsonConvert.DeserializeObject<Qotd>(quoteJson["contents"]["quotes"][0].ToString());

        Console.WriteLine($"Quote from {quote.Author}: {quote.Quote}");

        var botClient = new TelegramBotClient(config.ApiToken);
        //chatID: 96546887

        await botClient.SendTextMessageAsync(
            chatId: "96546887",
            text: quote.Quote,
            disableNotification: true,
            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Check google", "https://www.google.com"))
        );

    }
}