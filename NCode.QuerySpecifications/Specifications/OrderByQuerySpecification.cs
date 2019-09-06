using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public class OrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity, TProperty>
        where TEntity : class
    {
        public OrderByQuerySpecification(IReadOnlyList<IQuerySpecification<TEntity>> specifications, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending)
        {
            OutputSpecifications = specifications ?? throw new ArgumentNullException(nameof(specifications));
            KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

            Comparer = comparer;
            Descending = @descending;
        }

        public string Name => QueryNames.OrderBy;

        public IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications { get; }

        public Type PropertyType => typeof(TProperty);

        public Expression<Func<TEntity, TProperty>> KeySelector { get; }

        public IComparer<TProperty> Comparer { get; }

        public bool Descending { get; }
    }
}