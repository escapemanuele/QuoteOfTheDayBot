using System;
using Telegram.Bot.Types;


namespace QuoteOfTheDay
{

    public class MessageHandler
    {
        private Update Update;

        public MessageHandler(Update _update)
        {
            Update = _update;
        }


        public bool IsNull()
        {
            return (Update == null) || (Update.Message == null) || string.IsNullOrEmpty(Update.Message.Text);
        }

        public bool IsStartMessage()
        {
            return Update.Message.Text.Equals("/start");
        }
    }
}