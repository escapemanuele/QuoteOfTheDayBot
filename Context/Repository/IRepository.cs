using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteOfTheDay.Context.Repository
{
    public interface IRepository<TEntity>
    {

        bool Add(TEntity entity);

        bool CheckIfExists(long entityId);
    }
}
