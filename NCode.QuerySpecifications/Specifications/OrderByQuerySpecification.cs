using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public class OrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity, TProperty>
        where TEntity : class
    {
        public OrderByQuerySpecification(IReadOnlyList<IQuerySpecification<TEntity>> specifications, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer, bool @descending)
        {
            OutputSpecifications = specifications ?? throw new ArgumentNullException(nameof(specifications));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));

            Comparer = comparer;
            Descending = @descending;
        }

        public string Name => QueryNames.OrderBy;

        public IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications { get; }

        public Type PropertyType => typeof(TProperty);

        public Expression<Func<TEntity, TProperty>> Expression { get; }

        public IComparer<TProperty> Comparer { get; }

        public bool Descending { get; }
    }
}