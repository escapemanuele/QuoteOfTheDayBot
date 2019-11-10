using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteOfTheDay.Context
{
    public class QotdDbContext: DbContext
    {
        public QotdDbContext(DbContextOptions<QotdDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        
        public DbSet<Chat> Chats { get; set; }
    }
}
