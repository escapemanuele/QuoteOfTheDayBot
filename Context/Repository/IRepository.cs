using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteOfTheDay.Context.Repository
{
    public interface IRepository<TEntity>
    {

        void Add(TEntity entity);

        bool CheckIfExists(int entityId);
    }
}
