using CRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Contracts
{
    public interface ISpecifications<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; }

        Expression<Func<TEntity, bool>> Critera { get; }

        Expression<Func<TEntity, object>> OrderBy { get; }

        Expression<Func<TEntity, object>> OrderByDescending { get; }


        int Skip { get; }
        int Take { get; }

        bool ISPaginated { get; }
    }

}
