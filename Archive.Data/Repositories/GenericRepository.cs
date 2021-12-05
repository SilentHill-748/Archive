using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Archive.Data.Interfaces;

namespace Archive.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public void Add(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
