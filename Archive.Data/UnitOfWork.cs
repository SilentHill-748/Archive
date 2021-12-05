using Archive.Data.Interfaces;
using Archive.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

namespace Archive.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private Dictionary<Type, object>? _repositories;


        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context), "Контекст данных не может быть Null!");
        }


        public DbContext DbContext { get; }

        public IRepository<TEntity> Getrepository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
                _repositories = new Dictionary<Type, object>();

            Type entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
                _repositories[entityType] = new GenericRepository<TEntity>(DbContext);
            
            return (IRepository<TEntity>)_repositories[entityType];
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}
