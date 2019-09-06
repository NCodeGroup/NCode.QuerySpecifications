using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class OrderByQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TProperty>> _keySelector;
        private readonly IComparer<TProperty> _comparer;
        private readonly bool _descending;

        public OrderByQueryPipe(Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

            _comparer = comparer;
            _descending = @descending;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            if (queryRoot is IOrderedQueryable<TEntity> orderedQueryable)
            {
                return _descending
                    ? _comparer == null
                        ? orderedQueryable.ThenByDescending(_keySelector)
                        : orderedQueryable.ThenByDescending(_keySelector, _comparer)
                    : _comparer == null
                        ? orderedQueryable.ThenBy(_keySelector)
                        : orderedQueryable.ThenBy(_keySelector, _comparer);
            }

            return _descending
                ? _comparer == null
                    ? queryRoot.OrderByDescending(_keySelector)
                    : queryRoot.OrderByDescending(_keySelector, _comparer)
                : _comparer == null
                    ? queryRoot.OrderBy(_keySelector)
                    : queryRoot.OrderBy(_keySelector, _comparer);
        }

    }
}