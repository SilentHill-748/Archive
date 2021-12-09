using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Archive.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;

namespace Archive.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        private readonly DbSet<TEntity> _dbSet;


        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }


        public void Add(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (include is not null)
                return include(_dbSet);
            return _dbSet;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
