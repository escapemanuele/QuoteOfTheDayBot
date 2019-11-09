using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuoteOfTheDay.Context.Repository
{
    public class ChatRepository : IRepository<Chat>
    {
        readonly QotdDbContext dbContext;

        public ChatRepository(QotdDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Add(Chat entity)
        {
            if (!CheckIfExists(entity.ChatId))
            {
                dbContext.Chats.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public bool CheckIfExists(int entityId)
        {
            return dbContext.Chats.Any(x => x.ChatId == entityId);
        }
    }
}
