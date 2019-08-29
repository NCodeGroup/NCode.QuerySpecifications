using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Provider.Pipes
{
    public class OrderByQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        private readonly IComparer<TProperty> _comparer;
        private readonly bool _descending;

        public OrderByQueryPipe(Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer, bool @descending)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));

            _comparer = comparer;
            _descending = @descending;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            if (queryRoot is IOrderedQueryable<TEntity> orderedQueryable)
            {
                return _descending
                    ? _comparer == null
                        ? orderedQueryable.ThenByDescending(_expression)
                        : orderedQueryable.ThenByDescending(_expression, _comparer)
                    : _comparer == null
                        ? orderedQueryable.ThenBy(_expression)
                        : orderedQueryable.ThenBy(_expression, _comparer);
            }

            return _descending
                ? _comparer == null
                    ? queryRoot.OrderByDescending(_expression)
                    : queryRoot.OrderByDescending(_expression, _comparer)
                : _comparer == null
                    ? queryRoot.OrderBy(_expression)
                    : queryRoot.OrderBy(_expression, _comparer);
        }

    }
}