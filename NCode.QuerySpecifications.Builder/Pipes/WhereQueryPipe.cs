using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class WhereQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _predicate;

        public WhereQueryPipe(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Where(_predicate);
        }

    }
}