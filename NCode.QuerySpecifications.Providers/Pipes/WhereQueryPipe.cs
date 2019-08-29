using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Provider.Pipes
{
    public class WhereQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _expression;

        public WhereQueryPipe(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Where(_expression);
        }

    }
}