using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuoteOfTheDay.Context.Repository;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace QuoteOfTheDay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {

        private readonly ILogger<TelegramController> _logger;
        private readonly IRepository<QuoteOfTheDay.Context.Chat> db;
        private readonly BotConfiguration config;

        public TelegramController(
            ILogger<TelegramController> logger, 
            IRepository<QuoteOfTheDay.Context.Chat> _db,
            IOptions<BotConfiguration> _config)
        {
            _logger = logger;
            db = _db;
            config = _config.Value;
        }

        [HttpGet]
        public string Get()
        {
            return "ciao";
        }

        //    /start is the new message
        [HttpPost]
        public async Task<IActionResult> Post(Update update)
        {
            
            var msgHandler = new MessageHandler(update);
            if (!msgHandler.IsNull())
            {
                string botToken = Environment.GetEnvironmentVariable("ApiToken");
                var botClient = new TelegramBotClient(botToken);

                if(msgHandler.IsStartMessage())
                {
                    
                    var chat = new QuoteOfTheDay.Context.Chat { 
                            ChatId = update.Message.Chat.Id,
                            Name = update.Message.Chat.Username
                        };
                
                    string returnMessage = db.Add(chat)? $"Welcome aboard {chat.Name}!" : $"Welcome back {chat.Name}!";

                    await botClient.SendTextMessageAsync(
                        chatId: chat.ChatId,
                        text: returnMessage
                    );
                } 
                else 
                {
                    if (msgHandler.IsQotd()) 
                    {
                        string qotd = await QotdTask.GetQuoteOfTheDay();
                        await botClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: qotd
                        );
                    }
                }
            }

            return Ok();
        }
    }

}
