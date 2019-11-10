using System.Net.Http;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuoteOfTheDay;
using QuoteOfTheDay.Context;
using QuoteOfTheDay.Context.Repository;
using Telegram.Bot;

public class QotdTask : IInvocable
{
    private readonly ILogger<QotdTask> logger;

    private readonly BotConfiguration config;
    private readonly IRepository<Chat> db;

    public QotdTask(ILogger<QotdTask> _logger, IOptions<BotConfiguration> _config, IRepository<Chat> _db)
    {
        logger = _logger;
        config = _config.Value;
        db = _db;
    }

    public async Task Invoke()
    {
        logger.LogInformation("QotdTask launched");
        //Retrive the quote of the day
        var httpClient = new HttpClient();
        //System.Text.Json
        var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));
        var quote = JsonConvert.DeserializeObject<Qotd>(quoteJson["contents"]["quotes"][0].ToString());

        var botClient = new TelegramBotClient(config.ApiToken);
        //chatID: 96546887

        var chatsId = db.GetAll();

        foreach(var chatId in chatsId)
        {
            await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*{quote.Author}*:\n\n{quote.Quote}",
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    disableNotification: false
                );
        }
        
        logger.LogInformation($"Sent quote to {chatsId.Count} chats");
    }
}