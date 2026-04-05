using CRM.Core.Contracts;
using CRM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> entryPoint, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var Query = entryPoint;
            if (specifications is not null)
            {
                if (specifications.Critera is not null)
                {
                    Query = Query.Where(specifications.Critera);
                }

                if (specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0)
                {
                    #region First Syntax
                    //foreach (var includeExp in specifications.IncludeExpression)
                    //{
                    //    Query.Include(includeExp);
                    //} 
                    #endregion

                    Query = specifications.IncludeExpression
                        .Aggregate(Query, (CurrentQuery, includeExp) =>

                            CurrentQuery.Include(includeExp)
                        );
                }
                if (specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }

                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                }

                if (specifications.ISPaginated == true)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }

            return Query;

        }
    }
}
