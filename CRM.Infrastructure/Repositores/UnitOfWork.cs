using CRM.Core.Contracts;
using CRM.Core.Entities;
using CRM.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Repositores
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CRMdbContext _dbContext;

        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(CRMdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }

            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);

            _repositories[entityType] = newRepo;

            return newRepo;

        }

        public async Task<int> SaveChangesAsync() =>
          await _dbContext.SaveChangesAsync();

    }
}

