using CRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Contracts
{
    public interface IUnitOfWork
    {

        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
             where TEntity : BaseEntity<TKey>;

    }
}
