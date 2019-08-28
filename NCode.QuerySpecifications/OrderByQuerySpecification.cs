using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications
{
    public interface IOrderByQuerySpecification<TEntity> : IQuerySpecification<TEntity>
	    where TEntity : class
	{
        Type PropertyType { get; }
    }

    public class OrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity>
	    where TEntity : class
	{
        public string Name => QueryNames.OrderBy;

        public Type PropertyType => typeof(TProperty);

        public Expression<Func<TEntity, TProperty>> Expression { get; }

        public bool Descending { get; }

        public OrderByQuerySpecification(Expression<Func<TEntity, TProperty>> expression, bool @descending)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Descending = @descending;
        }

    }
}